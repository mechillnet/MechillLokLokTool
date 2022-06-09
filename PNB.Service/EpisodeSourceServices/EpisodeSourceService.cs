using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PNB.Service.EpisodeSourceServices
{

    public partial class EpisodeSourceService : IEpisodeSourceService
    {
        private readonly IRepository<EpisodeSource> _movieProductEpisodeSourceRepository;
        public EpisodeSourceService(IRepository<EpisodeSource> movieProductEpisodeSourceRepository)
        {
            _movieProductEpisodeSourceRepository = movieProductEpisodeSourceRepository;
        }
        public IList<EpisodeSource> GetByEpisodeId(int episodeId)
        {
            var query = from h in _movieProductEpisodeSourceRepository.Table where h.ProductEpisodeId == episodeId select h;
            return query.ToList();
        }

        public EpisodeSource GetById(int Id)
        {
            if (Id == 0)
                return null;
            return _movieProductEpisodeSourceRepository.GetById(Id);
        }
        public bool CheckExistVideoId(int SupplierId, string VideoId)
        {
            return (from h in _movieProductEpisodeSourceRepository.Table where h.Link == VideoId && h.SupplierId == SupplierId select h).Count() > 0;
        }
        public void Insert(EpisodeSource entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieProductEpisodeSourceRepository.Insert(entity);
        }
        public void Update(EpisodeSource entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieProductEpisodeSourceRepository.Update(entity);
        }
        public void Delete(EpisodeSource entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieProductEpisodeSourceRepository.Delete(entity.Id);
        }
    }
}
