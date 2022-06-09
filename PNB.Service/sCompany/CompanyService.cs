using PNB.Domain;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNB.Service.sCompany
{
         public partial  class CompanyService :ICompanyService
      {
        private readonly IRepository<Company> _repositoryCompany;
        public CompanyService(IRepository<Company> repositoryCompany)
        {
            _repositoryCompany = repositoryCompany;
        }
       public IPagedList<Company> GetAll( int start = 0, int lenght = 15)
        {
            var query = _repositoryCompany.Table;
           

            return new PagedList<Company>(query, start, lenght);

        }

        public virtual void Insert(IList<Company> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            _repositoryCompany.BulkInsert(list);
        }
        public void DeleteAll()
        {
            _repositoryCompany.ExecuteSqlCommand("TRUNCATE TABLE [Company]");
        }
    }
}
