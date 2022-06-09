using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sFiles
{
   public interface IFilesService
    {
        IPagedList<Files> GetAll(string filename="",int start=0,int take=15);

        Files GetFileById(int Id);
        void Insert(Files file);
        void Update(Files file);
        void Delete(Files file);
    }
}
