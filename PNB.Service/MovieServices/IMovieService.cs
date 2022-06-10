using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.MovieServices
{
    public interface IMovieService
    {
        IPagedList<Movie> GetAll(string search,bool? IsPublish, int? CategoryId, int? CountryId, int? TypeId, int? StatusId, string OrderBy, bool GetAnime = true, bool? isLokLok = null, int start = 0, int take = 15);
        IList<Movie> GetListAll(string search, bool? IsPublish, int? CategoryId, int? CountryId, int? TypeId, int? StatusId, string OrderBy, bool GetAnime = true, int start = 0, int take = 15);
        IPagedList<Movie> GetProductHomePageTop(int start = 0, int take = 15);
        IPagedList<Movie> GetProductCenterPage(int start = 0, int take = 15);
        IPagedList<Movie> GetProductNew(int start = 0, int take = 16);
        IPagedList<Movie> GetProductComingSoon(int start = 0, int take = 16);
    
        IList<Movie> GetProductRelated(int MovieId);
        Movie GetByLink(string link);
        IPagedList<Movie> GetProductByCastId(int CastId,int start = 0, int take = 15);
        bool CheckLinkProduct(string link);
        Movie GetById(int Id);
        void Insert(Movie entity);
        void Update(Movie entity);
        void Delete(Movie entity);
        //view
        Dictionary<int, int> Top10BestView(DateTime date);
        void InsertView(MovieView entity);
        bool checkLoklokMovie(string name, int? loklokId);
        //int GetViewMovie(int Id);
        //int GetRateMovie(int Id);

    }
}
