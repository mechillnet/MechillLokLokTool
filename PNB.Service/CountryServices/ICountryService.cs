using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.CountryServices
{
    public interface ICountryService
    {
        IPagedList<Country> GetAll(string search, int start = 0, int take = 15);

        Country GetById(int Id);
        Country GetByLink(string Link);
        void Insert(Country entity);
        void Update(Country entity);
        void Delete(Country entity);
    }
}
