using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Hosting;
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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Controllers.Client
{
    public class ToolController : BaseClientApiController
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
        public ToolController(IMovieService movieService,
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
        //public IActionResult UpdateAvatar()
        //{
        //    var list = _movieService.GetAll(search: null, IsPublish: null, CategoryId: null, CountryId: null, TypeId: null, StatusId: null, OrderBy: null, 0, int.MaxValue);
        //    string sWebRootFolder = _webHostEnvironment.WebRootPath;
        //    var folderName = _fileProvider.Combine(MediaDefaults.ImageMovieProductsPath);
        //    var pathToSave = _fileProvider.Combine(sWebRootFolder + MediaDefaults.ImageMovieProductsPath);
        //    foreach (var item in list)
        //    {

        //        var fullPath = _fileProvider.Combine(pathToSave, item.Link.ToString());
        //        var dbPath = _fileProvider.Combine(folderName, item.Link.ToString());

        //        var path = Path.Combine(_webHostEnvironment.WebRootPath, item.Avatar);
        //        var imageFileStream = System.IO.File.OpenRead(path);

        //        using (var stream = new FileStream("profile.jpg", FileMode.Create, FileAccess.Write, FileShare.Write, 4096))
        //        {
        //            stream.Write(imageBytes, 0, imageBytes.Length);
        //        }
        //    }
        //    return Ok();
        //}
        //[HttpGet("UpdateAvatar")]
        //public IActionResult UpdateAvatar()
        //{

        //    string sWebRootFolder = _webHostEnvironment.WebRootPath;
        //    var folderName = _fileProvider.Combine(MediaDefaults.ImageMovieProductsPath);



        //    var dbPath = _fileProvider.Combine(_webHostEnvironment.WebRootPath, folderName, "123.jpg");

        //    var path = _fileProvider.Combine(_webHostEnvironment.WebRootPath, @"/images\movie\truong-an-nhu-co-tung-tang3.jpg");
        //    var imageFileStream = System.IO.File.OpenRead(path);
        //    var imageBytes = _pictureService.ChangeSizeImage(imageFileStream);
        //    using (var stream = new FileStream(dbPath, FileMode.Create, FileAccess.Write, FileShare.Write, 4096))
        //    {
        //        stream.Write(imageBytes, 0, imageBytes.Length);
        //    }

        //    return Ok();
        //}

        [HttpGet("ImportImage")]
        public IActionResult ImportImage()
        {
            var movie = _movieService.GetAll(search: null, IsPublish: null, CategoryId: null, CountryId: null, TypeId: null, StatusId: null, OrderBy: null, true, 0, int.MaxValue);
            foreach (var m in movie)
            {
                if (m.Avatar != null)
                {
                    var AvatarPicture = new Picture();
                    var pathAvatar = _fileProvider.Combine(_webHostEnvironment.WebRootPath, m.Avatar);

                    var imageAvatarFileStream = System.IO.File.OpenRead(pathAvatar);
                    var imageAvatarBytes = _pictureService.ConvertoBinaryImage(imageAvatarFileStream);
                    AvatarPicture.Type = TypePicture.Avatar;
                    AvatarPicture.SeoFilename = m.Link + "-avatar";
                    AvatarPicture.TitleAttribute = AvatarPicture.SeoFilename;
                    AvatarPicture.AltAttribute = AvatarPicture.SeoFilename;
                    AvatarPicture.MimeType = Path.GetExtension(pathAvatar);
                    var AvatarPictureBinary = new PictureBinary();
                    AvatarPictureBinary.BinaryData = imageAvatarBytes;
                    AvatarPicture.PictureBinary = AvatarPictureBinary;
                    _pictureService.InsertPicture(AvatarPicture);
                    m.AvatarId = AvatarPicture.Id;

                }
                if (m.Panner != null)
                {
                    var PannerPicture = new Picture();
                    var pathPanner = _fileProvider.Combine(_webHostEnvironment.WebRootPath, m.Panner);
                    var imagePannerFileStream = System.IO.File.OpenRead(pathPanner);
                    var imagePannerBytes = _pictureService.ConvertoBinaryImage(imagePannerFileStream);
                    PannerPicture.Type = TypePicture.Panner;
                    PannerPicture.SeoFilename = m.Link + "-panner";
                    PannerPicture.TitleAttribute = PannerPicture.SeoFilename;
                    PannerPicture.AltAttribute = PannerPicture.SeoFilename;
                    PannerPicture.MimeType = Path.GetExtension(pathPanner);
                    var PannerPictureBinary = new PictureBinary();
                    PannerPictureBinary.BinaryData = imagePannerBytes;
                    PannerPicture.PictureBinary = PannerPictureBinary;

                    _pictureService.InsertPicture(PannerPicture);
                    m.PannerId = PannerPicture.Id;
                }
                _movieService.Update(m);
            }

            var cast = _castService.GetAll(search: null, 0, int.MaxValue);
            foreach (var c in cast)
            {
                if (c.Avatar != null)
                {
                    var AvatarPicture = new Picture();
                    var pathAvatar = _fileProvider.Combine(_webHostEnvironment.WebRootPath, c.Avatar);

                    var imageAvatarFileStream = System.IO.File.OpenRead(pathAvatar);
                    var imageAvatarBytes = _pictureService.ConvertoBinaryImage(imageAvatarFileStream);
                    AvatarPicture.Type = TypePicture.Avatar;
                    AvatarPicture.SeoFilename = c.Link + "-avatar";
                    AvatarPicture.TitleAttribute = AvatarPicture.SeoFilename;
                    AvatarPicture.AltAttribute = AvatarPicture.SeoFilename;
                    AvatarPicture.MimeType = Path.GetExtension(pathAvatar);
                    var AvatarPictureBinary = new PictureBinary();
                    AvatarPictureBinary.BinaryData = imageAvatarBytes;
                    AvatarPicture.PictureBinary = AvatarPictureBinary;
                    _pictureService.InsertPicture(AvatarPicture);
                    c.AvatarId = AvatarPicture.Id;

                }
                _castService.Update(c);
            }
            return Ok();
        }
        [HttpGet("ConvertKeyWord")]
        public IActionResult ConvertKeyWord()
        {
            var movie = _movieService.GetAll(search: null, IsPublish: null, CategoryId: null, CountryId: null, TypeId: null, StatusId: null, OrderBy: null, true, 0, int.MaxValue);
            foreach (var m in movie)
            {

                m.Keyword = ConvertKeywordMovie(m.Name, m.OtherName, m.TypeId, m.EpisodesTotal);
                _movieService.Update(m);
            }

            var episodes = _episodeService.GetAll(0, int.MaxValue);
            foreach (var ep in episodes)
            {
                ep.Keyword = ConvertKeywordEpisode(ep.Product.Name, ep.Product.OtherName, ep.EpisodeNumber, ep.Product.TypeId);
                _episodeService.Update(ep);
            }
            return Ok();
        }
        private string ConvertKeywordMovie(string MovieName, string OtherName, int? Type, int? EpisodeTotal)
        {
            var keyword = new List<string>();
            keyword.Add("xem phim " + MovieName.ToLower() + "full vietsub,thuyết minh,lồng tiếng,hd");
            if (!string.IsNullOrEmpty(OtherName))
            {
                keyword.Add(MovieName.ToLower() + "full vietsub,thuyết minh");
            }
            //phim bo
            if (Type == 2 && EpisodeTotal != null)
            {

                keyword.Add("phim " + MovieName.ToLower());

                for (int ep = 1; ep <= EpisodeTotal; ep++)
                {
                    keyword.Add("tập " + ep.ToString());
                }
                keyword.Add("tập cuối");
            }
            else
            {
                keyword.Add("phim " + MovieName.ToLower() + " full hd");
            }
            string[] webphim = { "mechill", "motchill", "motphim", "phimmoi", "tvhay", "hayghe", "subnhanh", "bichill", "vieon", "fptplay", "netflix", "youtube", "bilutv", "phim1080", "fullphim", "dongphim", "tvzing", "luotphim", "wetv" };
            keyword.AddRange(webphim);
            keyword.Add(_urlRecordService.GetSeName(MovieName).Replace("-", " ") + " vietsub,thuyết minh, thuyet minh");
            return string.Join(", ", keyword);
        }
        private string ConvertKeywordEpisode(string MovieName, string OtherName, int Episode, int? Type)
        {
            var keyword = new List<string>();
            string[] webphim = { "mechill", "motchill", "motphim", "phimmoi", "tvhay", "hayghe", "subnhanh", "bichill", "vieon", "fptplay", "netflix", "youtube", "bilutv", "phim1080", "fullphim", "dongphim", "tvzing", "luotphim", "wetv" };

            if (Type == 2)
            {
                keyword.Add("xem phim " + MovieName.ToLower() + " tập " + Episode + " vietsub");
                keyword.Add("xem phim " + MovieName.ToLower() + " tập " + Episode + " thuyết minh");
                keyword.Add("xem phim " + MovieName.ToLower() + " tập " + Episode + " lồng tiếng");
                keyword.Add("xem phim " + MovieName.ToLower() + " tập " + Episode + " hd");
                if (!string.IsNullOrEmpty(OtherName))
                {
                    keyword.Add(MovieName.ToLower() + " tập " + Episode + " vietsub");
                    keyword.Add(MovieName.ToLower() + " tập " + Episode + " thuyết minh");
                }
                keyword.AddRange(webphim);
                keyword.Add(_urlRecordService.GetSeName(MovieName).Replace("-", " ") + Episode + " vietsub,thuyết minh");
            }
            else
            {
                keyword.Add("xem phim " + MovieName.ToLower() + " full vietsub");
                keyword.Add("xem phim " + MovieName.ToLower() + " full thuyết minh");
                keyword.Add("xem phim " + MovieName.ToLower() + " full lồng tiếng");
                keyword.Add("xem phim " + MovieName.ToLower() + " full hd");
                if (!string.IsNullOrEmpty(OtherName))
                {
                    keyword.Add(MovieName.ToLower() + " full vietsub");
                    keyword.Add(MovieName.ToLower() + " full thuyết minh");
                }
                keyword.AddRange(webphim);
                keyword.Add(_urlRecordService.GetSeName(MovieName).Replace("-", " ") + Episode + " vietsub, thuyết minh, thuyet minh");
            }
            return string.Join(", ", keyword);
        }
        [HttpGet("ProcessAllPicture")]
        public IActionResult ProcessAllPicture()
        {
            var movie = _movieService.GetAll(search: null, IsPublish: null, CategoryId: null, CountryId: null, TypeId: null, StatusId: null, OrderBy: null, true, 0, int.MaxValue);

            foreach (var m in movie)
            {

                m.Avatar = _processCommon.MovieAvatarThumb(m.AvatarId.Value);
                m.Panner = _processCommon.MovieAvatarHorizontalThumb(m.PannerId.Value);
                _movieService.Update(m);
            }

            var cast = _castService.GetAll(null, 0, int.MaxValue);
            foreach (var c in cast)
            {
                c.Avatar = _processCommon.CastAvatar(c.AvatarId.Value);
                c.AvatarThumb = _processCommon.CastThumb(c.AvatarId.Value);
                _castService.Update(c);
            }
            return Ok();
        }

        [HttpGet("ProcessStatus")]
        public IActionResult ProcessStatus()
        {
            var movie = _movieService.GetAll(search: null, IsPublish: null, CategoryId: null, CountryId: null, TypeId: null, StatusId: null, OrderBy: null, true, 0, int.MaxValue);

            foreach (var m in movie)
            {

                var EpisodeCurrentVietSub = _episodeService.LastEpisode(m.Id, EpisodeType.VietSub);
                var EpisodeCurrentThuyetMinh = _episodeService.LastEpisode(m.Id, EpisodeType.ThuyetMinh);
                //phim le
                if (m.TypeId == 1)
                {
                    var title = "";
                    if (EpisodeCurrentVietSub == 0 && EpisodeCurrentThuyetMinh != 0)
                    {
                        title = "HD Thuyết Minh";
                    }
                    else if (EpisodeCurrentVietSub != 0 && EpisodeCurrentThuyetMinh == 0) {
                        title = "HD Vietsub";
                    }
                    else if (EpisodeCurrentVietSub != 0 && EpisodeCurrentThuyetMinh != 0)
                    {
                        title = "HD Vietsub (TM)";
                    }
                    else
                    {
                        title = "Đang cập nhật";
                    }
                    m.StatusTitle = title;
                }
                else
                {
                    var title = "";
                    if (EpisodeCurrentVietSub == 0 && EpisodeCurrentThuyetMinh != 0)
                    {
                        title = EpisodeCurrentThuyetMinh.ToString() + "/" + (m.EpisodesTotal > 0 ? m.EpisodesTotal.ToString() : "??") + " Thuyết Minh";
                    }
                    else if (EpisodeCurrentVietSub != 0 && EpisodeCurrentThuyetMinh == 0)
                    {
                        title = EpisodeCurrentVietSub.ToString() + "/" + (m.EpisodesTotal > 0 ? m.EpisodesTotal.ToString() : "??") + " Vietsub";
                    }
                    else if (EpisodeCurrentVietSub != 0 && EpisodeCurrentThuyetMinh != 0)
                    {
                        title = EpisodeCurrentVietSub.ToString() + "/" + (m.EpisodesTotal > 0 ? m.EpisodesTotal.ToString() : "??") + " Vietsub (" + EpisodeCurrentThuyetMinh + " TM)";
                    }
                    else
                    {
                        title = "Đang cập nhật";
                    }
                    m.StatusTitle = title;
                }

                _movieService.Update(m);
            }

            return Ok();
        }

        [HttpGet("ProcessSeokeywords")]
        public IActionResult ProcessSeokeywords()
        {
            var movie = _movieService.GetAll(search: null, IsPublish: null, CategoryId: null, CountryId: null, TypeId: null, StatusId: null, OrderBy: null, true, 0, int.MaxValue);

            foreach (var m in movie)
            {
                var list = new List<string>();
                list.Add(m.Name);
                list.Add(m.Name + " thuyết minh");
                var NameWithoutCharacter = m.Name.Replace(":", "").Replace("(", "").Replace(")", "");
                var SeoName = _urlRecordService.GetSeName(NameWithoutCharacter).Replace("-", " ");
                list.Add(SeoName + " vietsub");
                list.Add(SeoName + " " + m.Year);
                list.Add(m.OtherName + " " + m.Year + " hd vietsub");
                list.Add("phim " + SeoName);
                m.SeoKeywords = string.Join(", ", list);
                _movieService.Update(m);
            }

            return Ok();
        }

      
        [HttpGet("GetListMovieLokLok")]
        public IActionResult GetListMovieLokLok()
        {
            try
            {
                Uri uri = new Uri("https://ga-mobile-api.loklok.tv/cms/app/search/v1/search");
                string result = "";
                var stringContent = new StringContent(JsonConvert.SerializeObject(new
                {
                    size = 10000,
                   //@params= "COMIC",
                    order = "up",
                    area = "26,28,29,30,31,33,36,42,47,49,59"

                }), Encoding.UTF8, "application/json");
                using (var client = new HttpClient() { BaseAddress = uri })
                {
                    client.DefaultRequestHeaders.Add("versioncode", "11");
                    client.DefaultRequestHeaders.Add("clienttype", "ios_jike_default");
                    client.DefaultRequestHeaders.Add("lang", "en");
                    var response = client.PostAsync("", stringContent);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        result = response.Result.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        return null;
                    }
                }
                var reponseModel = JsonConvert.DeserializeObject<responseModelLokLok>(result).data.searchResults;
                Uri uriMovieDetail = new Uri("https://ga-mobile-api.loklok.tv/cms/app/movieDrama/");
                //   var listMovie = new List<MovieDetailLokLok>();
                //var allMovie = _movieService.GetAll(search: null, IsPublish: true, CategoryId: 1, CountryId: null, TypeId: null, StatusId: null, OrderBy: "Id", GetAnime: true, 0, int.MaxValue);
                foreach (var s in reponseModel)
                {
                    if (!_movieService.checkLoklokMovie(s.name,s.id))
                    {
                        using (var client = new HttpClient() { BaseAddress = uriMovieDetail })
                        {
                            client.DefaultRequestHeaders.Add("versioncode", "11");
                            client.DefaultRequestHeaders.Add("clienttype", "ios_jike_default");
                            client.DefaultRequestHeaders.Add("lang", "vi");
                            var response = client.GetAsync(string.Format("get?id={0}&category={1}", s.id, s.domainType));
                            if (response.Result.IsSuccessStatusCode)
                            {
                                result = response.Result.Content.ReadAsStringAsync().Result;
                            }
                            else
                            {
                                return null;
                            }
                            var movie = JsonConvert.DeserializeObject<responseMovie>(result).data;
                            if (movie != null)
                            {
                                var entityMovie = new Movie();
                                entityMovie.Avatar = movie.coverVerticalUrl + "?imageView2/1/w/380/h/532/format/webp/interlace/1/ignore-error/1/q/90!/format/webp";
                                entityMovie.Cdnavatar = entityMovie.Avatar;
                                entityMovie.Name = movie.name;
                                entityMovie.OtherName = s.name;
                                entityMovie.Year = movie.year;
                                entityMovie.Description = movie.introduction;
                                entityMovie.Panner = movie.coverHorizontalUrl + "?imageView2/1/w/532/h/380/format/webp/interlace/1/ignore-error/1/q/90!/format/webp";
                                entityMovie.Cdnbanner = entityMovie.Panner;
                                entityMovie.LokLokCategoryId = movie.category;
                                entityMovie.LokLokMovie = true;
                                entityMovie.IsPublish = true;
                                entityMovie.CreatedBy = 11;
                                entityMovie.LokLokMovieId = movie.id;
                                entityMovie.CountryId = 7;
                                entityMovie.SearchText = entityMovie.Name.ToLower() + "," + _urlRecordService.ConvertToSearch(entityMovie.Name);
                                var NameWithoutCharacter = movie.name.Replace(":", "").Replace("(", "").Replace(")", "");
                                entityMovie.Link = _urlRecordService.GetSeName(movie.name);
                                entityMovie.OriginalLink = entityMovie.Link;
                                if (_movieService.CheckLinkProduct(entityMovie.Link))
                                {
                                    entityMovie.Link = entityMovie.Link + DateTime.UtcNow.ToString("mmss");
                                }

                                entityMovie.CreatedOn = DateTime.UtcNow;
                                var ListEpisode = new List<Episode>();
                                var defaultEpisodeSource = new List<EpisodeSource>() { new EpisodeSource{
                        SupplierId= ServerUpload.LokLok,
                        CreateOn=DateTime.UtcNow,
                        CreatedBy=11,
                        IsIframe=false,
                        }};
                                if (movie.category == 0)
                                {
                                    entityMovie.StatusTitle = "Full HD";
                                    entityMovie.TypeId = 1;
                                    entityMovie.EpisodesTotal = 1;
                                    entityMovie.Status = 5;
                                    ListEpisode.Add(new Episode
                                    {
                                        Status = true,
                                        CreateOn = DateTime.UtcNow,
                                        CreateBy = 11,
                                        FullLink = entityMovie.Link + "-tap-full",
                                        Type = 1,
                                        EpisodeNumber = 0,
                                        Name = "Tập Full",
                                        EpisodeSource = defaultEpisodeSource
                                    });
                                }
                                else
                                {
                                    entityMovie.TypeId = 2;
                                    entityMovie.EpisodesTotal = movie.episodeCount;
                                    if (movie.episodeCount == movie.episodeVo.Count())
                                    {
                                        entityMovie.StatusTitle = "Full " + movie.episodeCount + "/" + movie.episodeCount + " Vietsub";
                                        entityMovie.Status = 5;
                                    }
                                    else
                                    {

                                        entityMovie.StatusTitle = "Tập " + movie.episodeVo.Count() + "/" + movie.episodeCount + " Vietsub";
                                        entityMovie.Status = 4;
                                    }
                                    foreach (var item in movie.episodeVo)
                                    {
                                        ListEpisode.Add(new Episode
                                        {
                                            Status = true,
                                            CreateOn = DateTime.UtcNow,
                                            CreateBy = 11,
                                            FullLink = entityMovie.Link + "-tap-" + item.seriesNo,
                                            Type = 1,
                                            EpisodeNumber = item.seriesNo.Value,
                                            Name = "Tập " + item.seriesNo,
                                            EpisodeSource = defaultEpisodeSource
                                        });
                                    }
                                }
                                entityMovie.Episode = ListEpisode;
                                var listCategoryMapping = new List<CategoryMapping>();
                                var allCategory = _categoryService.GetAll(null, 0, 999);
                                foreach (var ca in movie.tagList)
                                {
                                    var category = allCategory.Where(x => x.Description == ca.id.ToString()).FirstOrDefault();
                                    if (category != null)
                                    {
                                        listCategoryMapping.Add(new CategoryMapping()
                                        {
                                            CategoryId = category.Id
                                        });
                                    }

                                }

                                entityMovie.CategoryMapping = listCategoryMapping;

                                _movieService.Insert(entityMovie);
                            }
                        }

                    }


                }
                return Ok(reponseModel);
            }
            catch(Exception ex)
            {
                return ResponeSystemError();
            }
          
        }
        [HttpGet("InsertMovie")]
        public IActionResult InsertMovie(string MovieId,string CategoryId)
        {
            string result = "";
            Uri uriMovieDetail = new Uri("https://ga-mobile-api.loklok.tv/cms/app/movieDrama/");
            using (var client = new HttpClient() { BaseAddress = uriMovieDetail })
            {
                client.DefaultRequestHeaders.Add("versioncode", "11");
                client.DefaultRequestHeaders.Add("clienttype", "ios_jike_default");
                client.DefaultRequestHeaders.Add("lang", "vi");
                var response = client.GetAsync(string.Format("get?id={0}&category={1}", MovieId, CategoryId));
                if (response.Result.IsSuccessStatusCode)
                {
                    result = response.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return null;
                }
                var movie = JsonConvert.DeserializeObject<responseMovie>(result).data;
                if (movie != null)
                {
                    var entityMovie = new Movie();
                    entityMovie.Avatar = movie.coverVerticalUrl + "?imageView2/1/w/380/h/532/format/webp/interlace/1/ignore-error/1/q/90!/format/webp";
                    entityMovie.Cdnavatar = entityMovie.Avatar;
                    entityMovie.Name = movie.name;
                    entityMovie.OtherName = movie.name;
                    entityMovie.Year = movie.year;
                    entityMovie.Description = movie.introduction;
                    entityMovie.Panner = movie.coverHorizontalUrl + "?imageView2/1/w/532/h/380/format/webp/interlace/1/ignore-error/1/q/90!/format/webp";
                    entityMovie.Cdnbanner = entityMovie.Panner;
                    entityMovie.LokLokCategoryId = movie.category;
                    entityMovie.LokLokMovie = true;
                    entityMovie.IsPublish = true;
                    entityMovie.CreatedBy = 11;
                    entityMovie.LokLokMovieId = movie.id;
                    entityMovie.CountryId = 6;
                    entityMovie.SearchText = entityMovie.Name.ToLower() + "," + _urlRecordService.ConvertToSearch(entityMovie.Name);
                    var NameWithoutCharacter = movie.name.Replace(":", "").Replace("(", "").Replace(")", "");
                    entityMovie.Link = _urlRecordService.GetSeName(movie.name);
                    entityMovie.OriginalLink = entityMovie.Link;
                    if (_movieService.CheckLinkProduct(entityMovie.Link))
                    {
                        entityMovie.Link = entityMovie.Link + DateTime.UtcNow.ToString("mmss");
                    }

                    entityMovie.CreatedOn = DateTime.UtcNow;
                    var ListEpisode = new List<Episode>();
                    var defaultEpisodeSource = new List<EpisodeSource>() { new EpisodeSource{
                        SupplierId= ServerUpload.LokLok,
                        CreateOn=DateTime.UtcNow,
                        CreatedBy=11,
                        IsIframe=false,
                        }};
                    if (movie.category == 0)
                    {
                        entityMovie.StatusTitle = "Full HD";
                        entityMovie.TypeId = 1;
                        entityMovie.EpisodesTotal = 1;
                        entityMovie.Status = 5;
                        ListEpisode.Add(new Episode
                        {
                            Status = true,
                            CreateOn = DateTime.UtcNow,
                            CreateBy = 11,
                            FullLink = entityMovie.Link + "-tap-full",
                            Type = 1,
                            EpisodeNumber = 0,
                            Name = "Tập Full",
                            EpisodeSource = defaultEpisodeSource
                        });
                    }
                    else
                    {
                        entityMovie.TypeId = 2;
                        entityMovie.EpisodesTotal = movie.episodeCount;
                        if (movie.episodeCount == movie.episodeVo.Count())
                        {
                            entityMovie.StatusTitle = "Full " + movie.episodeCount + "/" + movie.episodeCount + " Vietsub";
                            entityMovie.Status = 5;
                        }
                        else
                        {

                            entityMovie.StatusTitle = "Tập " + movie.episodeVo.Count() + "/" + movie.episodeCount + " Vietsub";
                            entityMovie.Status = 4;
                        }
                        foreach (var item in movie.episodeVo)
                        {
                            ListEpisode.Add(new Episode
                            {
                                Status = true,
                                CreateOn = DateTime.UtcNow,
                                CreateBy = 11,
                                FullLink = entityMovie.Link + "-tap-" + item.seriesNo,
                                Type = 1,
                                EpisodeNumber = item.seriesNo.Value,
                                Name = "Tập " + item.seriesNo,
                                EpisodeSource = defaultEpisodeSource
                            });
                        }
                    }
                    entityMovie.Episode = ListEpisode;
                    var listCategoryMapping = new List<CategoryMapping>();
                    var allCategory = _categoryService.GetAll(null, 0, 999);
                    foreach (var ca in movie.tagList)
                    {
                        var category = allCategory.Where(x => x.Description == ca.id.ToString()).FirstOrDefault();
                        if (category != null)
                        {
                            listCategoryMapping.Add(new CategoryMapping()
                            {
                                CategoryId = category.Id
                            });
                        }

                    }

                    entityMovie.CategoryMapping = listCategoryMapping;

                    _movieService.Insert(entityMovie);
                }
            }
            return Ok();
        }
        //[HttpGet("SyncImageMovie")]
        //public ActionResult SyncImageMovie()
        //{
        //    var movie = _movieService.GetAll(search: null, IsPublish: null, CategoryId: null, CountryId: null, TypeId: null, StatusId: null, OrderBy: null, true, 0, int.MaxValue);
        //  string urlOriginal = "https://localhost:44381";
        //    Account account = new Account(
        //        "mechill-net-company",
        //        "418762924527573",
        //        "-dF07QmVsOr_dquxrRgxie_cMXo");

        //    Cloudinary cloudinary = new Cloudinary(account);
        //    cloudinary.Api.Secure = true;
        //    foreach (var m in movie)
        //    {
        //        if (!string.IsNullOrEmpty(m.Avatar))
        //        {
        //            string filePath = _fileProvider.GetAbsolutePath(m.Avatar.Replace(urlOriginal, ""));

        //            var uploadParams = new ImageUploadParams()
        //            {
        //                File = new FileDescription(filePath),
        //                Folder = "movie",
        //                Format = "webp",
        //            };
        //            var uploadResult = cloudinary.Upload(uploadParams);
        //            m.Cdnavatar = uploadResult.Url.OriginalString;
        //        }
        //        if (!string.IsNullOrEmpty(m.Panner))
        //        {
        //            string filePath = _fileProvider.GetAbsolutePath(m.Panner.Replace(urlOriginal, ""));

        //            var uploadParams = new ImageUploadParams()
        //            {
        //                File = new FileDescription(filePath),
        //                Folder = "movie",
        //                Format = "webp",
        //            };
        //            var uploadResult = cloudinary.Upload(uploadParams);
        //            m.Cdnbanner = uploadResult.Url.OriginalString;

        //        }
        //        _movieService.Update(m);
        //    }
        //    var cast = _castService.GetAll(search: null, 0, int.MaxValue).Where(x=>string.IsNullOrEmpty(x.CdnAvatarThumb));
        //    foreach (var c in cast)
        //    {
        //        if (!string.IsNullOrEmpty(c.Avatar))
        //        {
        //            string filePath = _fileProvider.GetAbsolutePath(c.Avatar.Replace(urlOriginal, ""));

        //            var uploadParams = new ImageUploadParams()
        //            {
        //                File = new FileDescription(filePath),
        //                Folder = "cast",
        //                Format = "webp",
        //            };
        //            var uploadResult = cloudinary.Upload(uploadParams);
        //            c.CdnAvatar = uploadResult.Url.OriginalString;
        //        }
        //        if (!string.IsNullOrEmpty(c.AvatarThumb))
        //        {
        //            string filePath = _fileProvider.GetAbsolutePath(c.AvatarThumb.Replace(urlOriginal, ""));

        //            var uploadParams = new ImageUploadParams()
        //            {
        //                File = new FileDescription(filePath),
        //                Folder = "cast",
        //                Format = "webp",
        //            };
        //            var uploadResult = cloudinary.Upload(uploadParams);

        //            c.CdnAvatarThumb = uploadResult.Url.OriginalString;
        //        }

        //        _castService.Update(c);
        //    }
        //    return Ok();
        //}
    }
}
