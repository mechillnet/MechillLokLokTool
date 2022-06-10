using Microsoft.EntityFrameworkCore;
using PNB.Domain;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PNB.Service.MovieServices
{

    public partial class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieProductRepository;
        private readonly IRepository<Cast> _movieCastRepository;
        private readonly IRepository<CastMapping> _movieCastMappingRepository;
        private readonly IRepository<MovieView> _movieViewRepository;
        private readonly IRepository<MovieRate> _movieRateRepository;
        public MovieService(IRepository<Movie> movieProductRepository,
            IRepository<Cast> movieCastRepository,
            IRepository<CastMapping> movieCastMappingRepository,
        IRepository<MovieView> movieViewRepository,
             IRepository<MovieRate> movieRateRepository
            )
        {
            _movieProductRepository = movieProductRepository;
            _movieCastRepository = movieCastRepository;
            _movieViewRepository = movieViewRepository;
            _movieRateRepository = movieRateRepository;
            _movieCastMappingRepository = movieCastMappingRepository;
        }
       public IPagedList<Movie> GetAll(string search, bool? IsPublish, int? CategoryId, int? CountryId, int? TypeId, int? StatusId, string OrderBy,bool GetAnime=true,bool? isLokLok =null, int start = 0, int take = 15)
        {
            var query = from h in _movieProductRepository.Table.Include(x => x.CategoryMapping) select h;
            if (IsPublish != null)
            {
                query = query.Where(x => x.IsPublish == IsPublish);
            }
            if (CategoryId != null)
            {
                query = query.Where(x => x.CategoryMapping.Any(x=>x.CategoryId == CategoryId));
            }
            if (isLokLok != null)
            {
                query = query.Where(x => x.LokLokMovie== isLokLok);
            }
            if (CountryId != null)
            {
                query = query.Where(x => x.CountryId == CountryId);
            }
            if (TypeId != null)
            {
                query = query.Where(x => x.TypeId == TypeId);
            }
            if (StatusId != null)
            {
                query = query.Where(x => x.Status == StatusId);
            }
            if (GetAnime==false)
            {
                query = query.Where(x => x.CategoryMapping.Any(x => x.CategoryId != 1));
            }
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.SearchText.Contains(search)||x.OtherName.Contains(search));
            switch (OrderBy)
            {
                case "Year":
                    query = query.OrderByDescending(x => x.Year);
                    break;
                case "UpdateDate":
                    query = query.OrderByDescending(x =>x.UpdateOn??x.CreatedOn);
                    break;
                case "View":
                    query = query.OrderByDescending(x => x.ViewNumber);
                    break;
                case "Like":
                    query = query.OrderByDescending(x => x.LikeNumber);
                    break;
                case "Rate":
                    query = query.OrderByDescending(x => x.RatePoint/x.RateNumner);
                    break;
                case "Id":
                    query = query.OrderByDescending(x => x.Id);
                    break;
                default:
                    query = query.OrderByDescending(x => x.UpdateOn);
                    break;
            }

            return new PagedList<Movie>(query, start, take);
        }

        public IList<Movie> GetListAll(string search, bool? IsPublish, int? CategoryId, int? CountryId, int? TypeId, int? StatusId, string OrderBy, bool GetAnime = true, int start = 0, int take = 15)
        {
            var query = from h in _movieProductRepository.Table.Include(x => x.CategoryMapping) select h;
            if (IsPublish != null)
            {
                query = query.Where(x => x.IsPublish == IsPublish);
            }
            if (CategoryId != null)
            {
                query = query.Where(x => x.CategoryMapping.Any(x => x.CategoryId == CategoryId));
            }
            if (CountryId != null)
            {
                query = query.Where(x => x.CountryId == CountryId);
            }
            if (TypeId != null)
            {
                query = query.Where(x => x.TypeId == TypeId);
            }
            if (StatusId != null)
            {
                query = query.Where(x => x.Status == StatusId);
            }
            if (GetAnime == false)
            {
                query = query.Where(x => x.CategoryMapping.Any(x => x.CategoryId != 1));
            }
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.SearchText.Contains(search) || x.OtherName.Contains(search));
            switch (OrderBy)
            {
                case "Year":
                    query = query.OrderByDescending(x => x.Year);
                    break;
                case "UpdateDate":
                    query = query.OrderByDescending(x => x.UpdateOn ?? x.CreatedOn);
                    break;
                case "View":
                    query = query.OrderByDescending(x => x.ViewNumber);
                    break;
                case "Like":
                    query = query.OrderByDescending(x => x.LikeNumber);
                    break;
                case "Rate":
                    query = query.OrderByDescending(x => x.RatePoint / x.RateNumner);
                    break;
                case "Id":
                    query = query.OrderByDescending(x => x.Id);
                    break;
                default:
                    query = query.OrderByDescending(x => x.UpdateOn);
                    break;
            }

            return query.Skip(start).Take(take).ToList();
        }
        public IPagedList<Movie> GetProductHomePageTop(int start = 0, int take = 15)
        {
            var query =_movieProductRepository.Table.Where(h=>h.ShowTop ==true && h.IsPublish == true).OrderByDescending(h=>h.DisplayOrder).ThenByDescending(h=>h.UpdateOn);
            return new PagedList<Movie>(query, start, take);
        }
        public IPagedList<Movie> GetProductCenterPage(int start = 0, int take = 15)
        {
            var query = _movieProductRepository.Table.Where(h=> h.ShowCenter == true && h.IsPublish == true).OrderBy(h=>h.DisplayOrder).ThenByDescending(h=>h.UpdateOn);
            return new PagedList<Movie>(query, start, take);
        }
        public IPagedList<Movie> GetProductNew(int start = 0, int take = 16)
        {
            var query = from h in _movieProductRepository.Table where h.IsPublish == true && h.IsNew ==true orderby h.UpdateOn descending select h;
            return new PagedList<Movie>(query, start, take);
        }
        public IPagedList<Movie> GetProductComingSoon(int start = 0, int take = 16)
        {
            var query = from h in _movieProductRepository.Table where h.IsPublish == true && (h.Status ==2||h.Status==3) && h.CategoryMapping.Any(x => x.CategoryId != 1) orderby h.UpdateOn descending select h;
            return new PagedList<Movie>(query, start, take);
        }

        public IList<Movie> GetProductRelated(int MovieId)
        {
            var product = (from p in _movieProductRepository.Table.Include(x=>x.CategoryMapping) where p.Id==MovieId select p).FirstOrDefault();
            if (product == null)
            {
                return null;
            }
            var FirstCategory = product.CategoryMapping.FirstOrDefault()?.CategoryId;
            var query = _movieProductRepository.Table.Include(x=>x.CategoryMapping).Where(x=> x.CategoryMapping.Any(y=>y.CategoryId == FirstCategory) &&x.CountryId == product.CountryId && x.TypeId== product.TypeId && x.Id!=product.Id);
            return query.OrderBy(elem => Guid.NewGuid()).Take(5).AsEnumerable().ToList();
        }
        public bool  CheckLinkProduct(string link)
        {
            return _movieProductRepository.Table.Where(x => x.Link == link).Count() > 0;
        }
        public Movie GetByLink(string link)
        {
            if (string.IsNullOrEmpty(link))
                return null;
            return _movieProductRepository.Table.Include(x => x.CategoryMapping).ThenInclude(x=>x.Category).Include(x => x.Country).Include(x => x.CastMapping).ThenInclude(y=>y.Cast).Include(x => x.Episode).FirstOrDefault(x => x.Link == link && x.IsPublish ==true);
        }
        public IPagedList<Movie> GetProductByCastId(int CastId, int start = 0, int take = 15)
        {
            var query = _movieCastMappingRepository.Table.Where(x => x.CastId == CastId && x.ProductId != null).Include(x => x.Product).Select(x => x.Product);
            return new PagedList<Movie>(query, start, take);
            
        }
        public Movie GetById(int Id)
        {
            var query = from p in _movieProductRepository.Table.Include(x => x.CategoryMapping).Include(x => x.Country).Include(x => x.Episode).Include(x => x.CategoryMapping).Include(x => x.CastMapping).ThenInclude(x=>x.Cast)
                        where p.Id == Id
                        select p;
            if (Id == 0)
                return null;
            return query.FirstOrDefault();
        }
        public void Insert(Movie entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieProductRepository.Insert(entity);
        }
        public void Update(Movie entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieProductRepository.Update(entity);
        }
        public void Delete(Movie entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieProductRepository.Delete(entity.Id);
        }
        public Dictionary<int, int> Top10BestView(DateTime date)
        {
            var query = _movieViewRepository.Table.Include(X=>X.Product).Where(x=>x.ViewAt>date).GroupBy(x => x.ProductId).OrderByDescending(x=>x.Count()).Select(g => new { Key = g.Key, Count =g.Count()}).Take(10).ToList();
            var movie = new Dictionary<int,int>();
            foreach(var item in query)
            {
                movie.Add(item.Key.Value, item.Count);
            }
            //var product = _movieProductRepository.Table.Where(x => query.Any(y => y == x.Id)).ToList().OrderByDescending(x=> query);
            return movie;
        }
        public void InsertView(MovieView entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieViewRepository.Insert(entity);
        }
        public bool checkLoklokMovie(string name, int? loklokId)
        {
            var check = _movieProductRepository.Table.Any(x => x.LokLokMovieId == loklokId || x.Name.ToLower() == name.ToLower() || x.OtherName.ToLower() == name.ToLower());
            return check;
        }
        //public int GetViewMovie(int Id)
        //{
        //    return  _movieViewRepository.Table.Where(x => x.ProductId == Id).Count();
        //}
        //public int GetRateMovie(int Id)
        //{
        //    return _movieRateRepository.Table.Where(x => x.ProductId == Id).Count();
        //}
    }
}
