using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sTicket
{
   public interface ITicketService
    {
        IPagedList<TTicket> GetAll(string search, int? statusId, int? categoryId, int? typeId, int? priorityId, string OrderBy, bool isDelete = false, int start = 0, int take = 15);
        TTicket GetById(int Id);
        void Insert(TTicket entity);
        void Update(TTicket entity);
        void Delete(TTicket entity);
        //view
     
    }
}
