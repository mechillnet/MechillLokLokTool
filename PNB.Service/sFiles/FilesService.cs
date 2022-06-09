using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PNB.Domain;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PNB.Service.sFiles
{
   public partial class FilesService :IFilesService
    {
        private readonly IRepository<Files> _filesrepository;
        public FilesService(IRepository<Files> filesrepository)
        {
            _filesrepository = filesrepository;
        }
       public IPagedList<Files> GetAll(string filename = "", int start = 0, int take = 15)
        {
            var query = from f in _filesrepository.Table select f;
            if (!string.IsNullOrEmpty(filename))
            {
                query = query.Where(x => x.Filename.Contains(filename));
            }
            return new PagedList<Files>(query, start, take);

        }

        public Files GetFileById(int Id)
        {
            if (Id == 0)
            {
                return null;
            }
            var file = _filesrepository.GetById(Id);
            return file != null ? _filesrepository.GetById(Id) : null;
        }

        public void Insert(Files file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            _filesrepository.Insert(file);

        }
        public void Update(Files file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            _filesrepository.Update(file);
        }
        public void Delete(Files file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            _filesrepository.Delete(file.Id);
        }
    }
}
