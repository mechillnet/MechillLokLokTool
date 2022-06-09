using Microsoft.EntityFrameworkCore;
using PNB.Domain;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using PNB.Service.sSeo;
using System;
using System.Linq;

namespace PNB.Service.EpisodeServices
{
    public partial class EpisodeService : IEpisodeService
    {
        private readonly IRepository<Episode> _movieProductEpisodeRepository;
      //  private readonly IUrlRecordService _urlRecordService;
        public EpisodeService(IRepository<Episode> movieProductEpisodeRepository/*, IUrlRecordService urlRecordService*/)
        {
            _movieProductEpisodeRepository = movieProductEpisodeRepository;
          //  _urlRecordService = urlRecordService;
        }
        public IPagedList<Episode> GetAll(int start = 0, int take = 15)
        {
            var query = from h in _movieProductEpisodeRepository.Table.Include(x=>x.Product) where !string.IsNullOrEmpty(h.Link) orderby h.CreateOn descending select h;
            return new PagedList<Episode>(query, start, take);
        }
        public IPagedList<Episode> GetByProductId(int ProductId,int Type, int start = 0, int take = 15)
        {
            var query = from h in _movieProductEpisodeRepository.Table.Include(x => x.EpisodeSource) where h.ProductId == ProductId &&h.Type==Type orderby h.EpisodeNumber descending select h;
            return new PagedList<Episode>(query, start, take);
        }

        public Episode GetById(int Id)
        {
            if (Id == 0)
                return null;
            var query = from h in _movieProductEpisodeRepository.Table.Include(x => x.EpisodeSource) where h.Id == Id select h;
            return query.FirstOrDefault();
        }
        public Episode GetByEpisode(int productId, int EpisodeNumber)
        {
            var query = from h in _movieProductEpisodeRepository.Table.Include(x => x.EpisodeSource).Include(x => x.Product) where h.ProductId == productId && h.EpisodeNumber == EpisodeNumber select h;
            return query.FirstOrDefault();
        }
        public Episode GetByEpisode(int productId, int EpisodeNumber,int Type)
        {
            var query = from h in _movieProductEpisodeRepository.Table.Include(x => x.EpisodeSource).Include(x => x.Product) where h.ProductId == productId && h.EpisodeNumber == EpisodeNumber && h.Type ==Type select h;
            return query.FirstOrDefault();
        }
        public Episode GetFirstEpisode(int productId)
        {
            var query = from h in _movieProductEpisodeRepository.Table.Include(x => x.EpisodeSource).Include(x => x.Product) where h.ProductId == productId orderby h.EpisodeNumber select h;
            return query.FirstOrDefault();
        }
        public Episode GetEpisodeByLink(int productId, string Link)
        {
            var query = from h in _movieProductEpisodeRepository.Table.Include(x => x.EpisodeSource).Include(x=>x.Product) where h.ProductId == productId && h.Link == Link select h;
            return query.FirstOrDefault();
        }
        public Episode GetEpisodeByFullLink(string Link)
        {
            var query = from h in _movieProductEpisodeRepository.Table.Include(x => x.EpisodeSource).Include(x => x.Product) where h.FullLink == Link select h;
            return query.FirstOrDefault();
        }
        public bool CheckExistLinkEpisode(int productId, string Link)
        {
            var query = from h in _movieProductEpisodeRepository.Table where h.ProductId == productId && h.Link == Link select h;
            return query.Count()>0;
        }
        public bool CheckExistLinkEpisode(int productId,int episodeNumber, string Link)
        {
            var query = from h in _movieProductEpisodeRepository.Table where h.ProductId == productId && h.Link == Link && h.EpisodeNumber!= episodeNumber select h;
            return query.Count() > 0;
        }
        public bool CheckExistEpisodeNumber(int productId, int EpisodeNumber,int Type)
        {
            var query = from h in _movieProductEpisodeRepository.Table where h.ProductId == productId && h.EpisodeNumber == EpisodeNumber &&h.Type== Type select h;
            return query.Count() > 0;
        }
        public void Insert(Episode entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieProductEpisodeRepository.Insert(entity);
        }
        public void Update(Episode entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieProductEpisodeRepository.Update(entity);
        }
        public void Delete(Episode entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieProductEpisodeRepository.Delete(entity.Id);
        }
        public int LastEpisode(int productId,int Type)
        {
            return _movieProductEpisodeRepository.Table.Where(x => x.ProductId == productId&&x.Status==true&&x.Type==Type).OrderByDescending(x=>x.EpisodeNumber).FirstOrDefault()?.EpisodeNumber??0;
        }
        //public void  initSeoLink()
        //{
        //    var episode = _movieProductEpisodeRepository.Table.ToList();
        //    foreach(var item in episode)
        //    {
        //        item.Link = _urlRecordService.GetSeName(item.Name);
        //        _movieProductEpisodeRepository.Update(item);
        //    }
          
        //}
    }
}
