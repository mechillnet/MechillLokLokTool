using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhamNgocBau.Api.Factories;
using PhamNgocBau.Api.Model.LokLok;
using PNB.Domain.Common;
using PNB.Domain.Common.Movie;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using PNB.Service.CastServices;
using PNB.Service.CategoryServices;
using PNB.Service.EpisodeServices;
using PNB.Service.MovieServices;
using PNB.Service.sAuthenticationService;
using PNB.Service.sPicture;
using PNB.Service.sSeo;
using PNB.Service.sSetting;
using PNB.Service.sUserService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace PhamNgocBau.Api.Controllers
{
    public class MovieController : BaseClientApiController
    {
        #region ctor
        private readonly IMovieService _movieService;
        //private readonly IMovie_CategoryService _categoryService;
        //private readonly IMovie_CountryService _countryService;
        private readonly IEpisodeService _episodeService;
        //private readonly IMovie_ProductEpisodeSourceService _episodeSourceService;
        private readonly IPNBFileProvider _fileProvider;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISettingService _settingService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;
        private readonly ICastService _castService;
        private readonly IProcessCommon _processCommon;
        private readonly ICategoryService _categoryService;
        //private readonly IFacebookApiService _facebookApiService;
        public MovieController(IMovieService movieService,
            //IMovie_CategoryService categoryService,
            //IMovie_CountryService countryService,
            IEpisodeService episodeService,
            // IMovie_ProductEpisodeSourceService episodeSourceService,
            IPNBFileProvider fileProvider,
            IUserService userService,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            ISettingService settingService,
            IUrlRecordService urlRecordService,
            IWorkContext workContext,
            IPictureService pictureService,
            ICastService castService,
            IProcessCommon processCommon,
            ICategoryService categoryService
            //IFacebookApiService facebookApiService
            )
        {
            _movieService = movieService;
            //_categoryService = categoryService;
            //_countryService = countryService;
            _episodeService = episodeService;
            //_episodeSourceService = episodeSourceService;
            _fileProvider = fileProvider;
            _userService = userService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _settingService = settingService;
            _urlRecordService = urlRecordService;
            _workContext = workContext;
            _pictureService = pictureService;
            _castService = castService;
            _processCommon = processCommon;
            _categoryService = categoryService;
            // _facebookApiService = facebookApiService;
        }
        #endregion
        [HttpGet("ProcessLokLok")]
        public IActionResult ProcessLokLok()
        {
            var movie = _movieService.GetAll(search: null, IsPublish: null, CategoryId: null, CountryId: null, TypeId: null, StatusId: null, OrderBy: null, true, true, 0, int.MaxValue);
            var path = _fileProvider.MapPath("/images/loklok/");
            foreach (var m in movie.Take(10))
            {
                if (m.Avatar.Contains("img.netpop.app"))
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri(m.Avatar), path +"avatar-"+m.Link+".jpg" );
                    }
                }
            }
            return Ok();
        }

        private void UploadToServer(IFormFile file)
        {
            string url = "ftp://ftp.example.com/remote/path/file.zip";
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential("xxxx", "xxxx");
                using (var ftpStream = client.OpenWrite(url))
                {
                    file.CopyTo(ftpStream);
                }
            }
        }
    }
}
