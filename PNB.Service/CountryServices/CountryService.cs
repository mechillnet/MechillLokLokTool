using PNB.Domain;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PNB.Service.CountryServices
{
    public partial class CountryService : ICountryService
    {
        private readonly IRepository<Country> _movieCountryRepository;
        public CountryService(IRepository<Country> MovieCountryRepository)
        {
            _movieCountryRepository = MovieCountryRepository;
        }
        public IPagedList<Country> GetAll(string search, int start = 0, int take = 15)
        {
            var query = from h in _movieCountryRepository.Table select h;
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Name.Contains(search));

            return new PagedList<Country>(query, start, take);
        }
        public Country GetByLink(string Link)
        {
            var query = from h in _movieCountryRepository.Table  where h.Link == Link select h;
            return query.FirstOrDefault();
        }
        public Country GetById(int Id)
        {
            if (Id == 0)
                return null;
            return _movieCountryRepository.GetById(Id);
        }
        public void Insert(Country entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieCountryRepository.Insert(entity);
        }
        public void Update(Country entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieCountryRepository.Update(entity);
        }
        public void Delete(Country entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieCountryRepository.Delete(entity.Id);
        }
    }
}
