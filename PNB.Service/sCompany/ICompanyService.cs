using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sCompany
{
    public interface ICompanyService
    {
        IPagedList<Company> GetAll(int start = 0, int lenght = 15);

        void Insert(IList<Company> list);

        void DeleteAll();
    }
}
