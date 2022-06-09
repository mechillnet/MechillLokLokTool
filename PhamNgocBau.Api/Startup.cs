using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhamNgocBau.Api.Extension;
using PhamNgocBau.Api.Extension.ApiSetting;
using PhamNgocBau.Api.Extension.Infrastructure.Mapper;
using PhamNgocBau.Api.Factories;
using PNB.Domain;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using PNB.Service.AdsServices;
using PNB.Service.CastServices;
using PNB.Service.CategoryServices;
using PNB.Service.CountryServices;
using PNB.Service.EpisodeServices;
using PNB.Service.EpisodeSourceServices;
using PNB.Service.FacebookApi;
using PNB.Service.HistoryLinkServices;
using PNB.Service.MovieServices;
using PNB.Service.sAuthenticationService;
using PNB.Service.Security;
using PNB.Service.sPicture;
using PNB.Service.sRoles;
using PNB.Service.sSelectOption;
using PNB.Service.sSendEmail;
using PNB.Service.sSeo;
using PNB.Service.sSetting;
using PNB.Service.sUserService;
using PNB.Web.Framework.Extensions;
using System.Text;
using System.Threading.Tasks;

namespace PhamNgocBau.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _webHostEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment _webHostEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingAPIProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
            services.AddCors();
            services.AddMvc(); ;
            services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null); ;
            services.AddDbContext<DbPhim77>(opts => opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ISelectOptionService, SelectOptionService>();
            services.AddScoped<IAuthenticationService, CookieAuthenticationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IWorkContext, WebWorkContextApi>();
            services.AddScoped<IPNBFileProvider, PNBFileProvider>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ISendEmailService, SendEmailService>();
            services.AddScoped<ICipherService, CipherService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUrlRecordService, UrlRecordService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<IAdsService, AdsService>();
            #region Movie
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IEpisodeService, EpisodeService>();
            services.AddScoped<IEpisodeSourceService, EpisodeSourceService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICastService, CastService>();
            services.AddScoped<IFacebookApiService, FacebookApiService>();
            services.AddScoped<IProcessCommon, ProcessCommon>();
            services.AddScoped<IHistoryLinkService, HistoryLinkService>();
            #endregion

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            if (_webHostEnvironment.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "My API",
                        Version = "v1"
                    });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",


                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                 {
               new OpenApiSecurityScheme
               {Reference = new OpenApiReference
               {Type = ReferenceType.SecurityScheme,
               Id = "Bearer"}},new string[] { }}});
                    // default value header for test
                    c.OperationFilter<DefaultHeaderFilter>();
                });
            }
            #region authorize
            var sp = services.BuildServiceProvider();
            var configApi = (ISettingService)sp.GetService(typeof(ISettingService));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configApi.LoadSetting<ConfigurationApi>().Key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        //string mKey = context.HttpContext.Request.Query["TokenLogin"].ToString();
                        //if (string.IsNullOrEmpty(mKey) && context.HttpContext.Request.ContentType != null)
                        //    mKey = context.HttpContext.Request.Form["TokenLogin"].ToString();
                        //if (string.IsNullOrEmpty(mKey))
                        var mKey = context.HttpContext.Request.Headers["Authentication"].ToString();
                        if (!string.IsNullOrEmpty(mKey))
                        {
                            context.Token = mKey;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            #endregion

            #region google auth
            //services
            //.AddAuthentication(o =>
            //{
            //    // This forces challenge results to be handled by Google OpenID Handler, so there's no
            //    // need to add an AccountController that emits challenges for Login.
            //    o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
            //    // This forces forbid results to be handled by Google OpenID Handler, which checks if
            //    // extra scopes are required and does automatic incremental auth.
            //    o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
            //    // Default scheme that will handle everything else.
            //    // Once a user is authenticated, the OAuth2 token info is stored in cookies.
            //    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            //})
            //.AddCookie()
            //.AddGoogleOpenIdConnect(options =>
            //{
            //    options.ClientId = "312692644823-7dstuttj4cr1v0ljrq8rhotb0ltjtqbt.apps.googleusercontent.com";
            //    options.ClientSecret = "B2jT3Fr0nzZ8Fbf-5svsNt2-";

            //});

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x
          .AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowed(origin => true) // allow any origin
          .AllowCredentials()); // allow credentials


           // app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //// enable swagger
            //if (env.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            //}
        }
    }
}
