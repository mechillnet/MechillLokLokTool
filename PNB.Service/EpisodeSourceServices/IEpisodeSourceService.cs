using PNB.Domain.Models;
using System.Collections.Generic;

namespace PNB.Service.EpisodeSourceServices
{
    public interface IEpisodeSourceService
    {
        IList<EpisodeSource> GetByEpisodeId(int EpisodeId);

        EpisodeSource GetById(int Id);
        bool CheckExistVideoId(int SupplierId, string VideoId);
        void Insert(EpisodeSource entity);
        void Update(EpisodeSource entity);
        void Delete(EpisodeSource entity);
    }
}
