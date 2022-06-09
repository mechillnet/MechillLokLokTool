using PNB.Domain.Infrastructure;
using PNB.Domain.Models;

namespace PNB.Service.CastServices
{
    public interface ICastService
    {
        IPagedList<Cast> GetAll(string search, int start = 0, int take = 15);

        Cast GetById(int Id);
        Cast GetByLink(string Link);
        void Insert(Cast entity);
        void Update(Cast entity);
        void Delete(Cast entity);
    }
}
