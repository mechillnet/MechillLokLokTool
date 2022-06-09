using PNB.Domain.Infrastructure;
using PNB.Domain.Models;

namespace PNB.Service.EpisodeServices
{
    public interface IEpisodeService
    {
        IPagedList<Episode> GetAll(int start = 0, int take = 15);
        IPagedList<Episode> GetByProductId(int ProductId,int Type, int start = 0, int take = 15);

        Episode GetById(int Id);
        Episode GetByEpisode(int productId, int EpisodeNumber);
        Episode GetByEpisode(int productId, int EpisodeNumber,int Type);
        Episode GetFirstEpisode(int productId);
        Episode GetEpisodeByLink(int productId, string Link);
        Episode GetEpisodeByFullLink(string Link);
        bool CheckExistLinkEpisode(int productId, string Link);
        bool CheckExistLinkEpisode(int productId, int episodeNumber, string Link);
        bool CheckExistEpisodeNumber(int productId, int Episode,int Type);
        void Insert(Episode entity);
        void Update(Episode entity);
        void Delete(Episode entity);
        int LastEpisode(int productId,int Type);
        // void initSeoLink();
    }
}
