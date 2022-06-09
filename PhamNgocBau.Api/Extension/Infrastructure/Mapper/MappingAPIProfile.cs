using AutoMapper;
using PhamNgocBau.Api.Model.Admin.Picture;
using PhamNgocBau.Api.Model.Admin.Setting;
using PhamNgocBau.Api.Model.Cast;
using PhamNgocBau.Api.Model.Category;
using PhamNgocBau.Api.Model.Country;
using PhamNgocBau.Api.Model.Episode;
using PhamNgocBau.Api.Model.EpisodeSource;
using PhamNgocBau.Api.Model.Movie;
using PhamNgocBau.Api.Model.Movie.Product;
using PhamNgocBau.Api.Model.OptionSelect;
using PhamNgocBau.Api.Model.Permission;
using PhamNgocBau.Api.Model.User;
using PNB.Domain.Models;

namespace PhamNgocBau.Api.Extension.Infrastructure.Mapper
{
    public partial class MappingAPIProfile : Profile
    {
        public MappingAPIProfile()
        {
            //client
            CreateMap<User, UserDtoModel>();
            CreateMap<UserDtoModel, User>();

            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();

            CreateMap<SelectOption, OptionSelectModel>();
            CreateMap<OptionSelectModel, SelectOption>();
            //admin

            CreateMap<Settings, SettingModel>();
            CreateMap<SettingModel, Settings>();

            CreateMap<Picture, PictureModel>();
            CreateMap<PictureModel, Picture>();

            CreateMap<Picture, PictureDtoModel>();
            CreateMap<PictureDtoModel, Picture>();

            // movie 
            CreateMap<Movie, MovieModel>().ForMember(x => x.CategoryMapping, opt => opt.Ignore()).ForMember(x => x.CastMapping, opt => opt.Ignore());
            CreateMap<MovieModel, Movie>().ForMember(x => x.CategoryMapping, opt => opt.Ignore()).ForMember(x => x.CastMapping, opt => opt.Ignore()); ;

            CreateMap<Movie, MovieDtoModel>();
            CreateMap<MovieDtoModel, Movie>();

            CreateMap<Country, CountryDtoModel>();
            CreateMap<CountryDtoModel, Country>();

            CreateMap<Category, CategoryDtoModel>();
            CreateMap<CategoryDtoModel, Category>();

            CreateMap<Episode, EpisodeModel>();
            CreateMap<EpisodeModel, Episode>();

            CreateMap<Episode, EpisodeDtoModel>();
            CreateMap<EpisodeDtoModel, Episode>();

            CreateMap<Episode, EpisodeModel>();
            CreateMap<EpisodeModel, Episode>();

            CreateMap<EpisodeSource, EpisodeSourceDtoModel>();
            CreateMap<EpisodeSourceDtoModel, EpisodeSource>();

            CreateMap<EpisodeSource, EpisodeSourceModel>();
            CreateMap<EpisodeSourceModel, EpisodeSource>();

            CreateMap<CastMapping, CastMappingModel>()
                .ForMember(d => d.CastName, a => a.MapFrom(s => s.Cast.Name))
                .ForMember(d => d.CastLink, a => a.MapFrom(s => s.Cast.Link))
                .ForMember(d => d.CastAvatar, a => a.MapFrom(s => s.Cast.Avatar))
                .ForMember(d => d.CastAvatarThumb, a => a.MapFrom(s => s.Cast.AvatarThumb))
               .ForMember(d => d.CastAvatarId, a => a.MapFrom(s => s.Cast.AvatarId))
             .ForMember(d => d.CdnAvatar, a => a.MapFrom(s => s.Cast.CdnAvatar))
               .ForMember(d => d.CdnAvatarThumb, a => a.MapFrom(s => s.Cast.CdnAvatarThumb));

            CreateMap<CastMappingModel, CastMapping>();

            CreateMap<CategoryMapping, CategoryMappingModel>()
                .ForMember(d => d.CategoryName, a => a.MapFrom(s => s.Category.Name))
                .ForMember(d => d.CategoryLink, a => a.MapFrom(s => s.Category.Link));
            CreateMap<CategoryMappingModel, CategoryMapping>();

            CreateMap<Cast, CastDtoModel>();
            CreateMap<CastDtoModel, Cast>();

            CreateMap<Cast, CastModel>();
            CreateMap<CastModel, Cast>();


        }

    }
}
