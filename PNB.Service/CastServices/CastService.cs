using PNB.Domain;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Linq;

namespace PNB.Service.CastServices
{
    public partial class CastService : ICastService
    {
        private readonly IRepository<Cast> _movieCastRepository;
        public CastService(IRepository<Cast> MovieCountryRepository)
        {
            _movieCastRepository = MovieCountryRepository;
        }
        public IPagedList<Cast> GetAll(string search, int start = 0, int take = 15)
        {
            var query = from h in _movieCastRepository.Table select h;
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Name.Contains(search));

            return new PagedList<Cast>(query.OrderByDescending(x=>x.UpdateOn??x.CreateOn), start, take);
        }
        public Cast GetByLink(string Link)
        {
            var query = from h in _movieCastRepository.Table where h.Link == Link select h;
            return query.FirstOrDefault();
        }
        public Cast GetById(int Id)
        {
            if (Id == 0)
                return null;
            return _movieCastRepository.GetById(Id);
        }
        public void Insert(Cast entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieCastRepository.Insert(entity);
        }
        public void Update(Cast entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieCastRepository.Update(entity);
        }
        public void Delete(Cast entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieCastRepository.Delete(entity.Id);
        }
    }
}
