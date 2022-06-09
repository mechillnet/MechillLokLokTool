using Microsoft.EntityFrameworkCore;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using PNB.Domain;

namespace PNB.Service.sTicket
{

    public partial class TicketService : ITicketService
    {
        private readonly IRepository<TTicket> _ticketRepository;
       
        public TicketService(IRepository<TTicket> ticketRepository
            )
        {
            _ticketRepository = ticketRepository;
         
        }
        public IPagedList<TTicket> GetAll(string search, int? statusId, int? categoryId, int? typeId, int? priorityId, string OrderBy,bool isDelete=false, int start = 0, int take = 15)
        {
            var query = from h in _ticketRepository.Table.Include(x => x.Status).Include(x => x.Type).Include(x => x.Priority).Include(x => x.Category) where h.IsDeleted == isDelete select h;
            if (statusId != null)
            {
                query = query.Where(x => x.StatusId == statusId);
            }
            if (categoryId != null)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }
            if (typeId != null)
            {
                query = query.Where(x => x.TypeId == typeId);
            }
            if (priorityId != null)
            {
                query = query.Where(x => x.PriorityId == priorityId);
            }
           
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Name.Contains(search)||x.Id.ToString()==search);
            switch (OrderBy)
            {
               
                default:
                    query = query.OrderByDescending(x => x.CreateOn);
                    break;
            }

            return new PagedList<TTicket>(query, start, take);
        }
   

        
        public TTicket GetById(int Id)
        {
            var query = from p in _ticketRepository.Table.Include(x => x.Status).Include(x => x.Type).Include(x => x.Priority).Include(x => x.Category)
                        where p.Id == Id
                        select p;
            if (Id == 0)
                return null;
            return query.FirstOrDefault();
        }
        public void Insert(TTicket entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _ticketRepository.Insert(entity);
        }
        public void Update(TTicket entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _ticketRepository.Update(entity);
        }
        public void Delete(TTicket entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _ticketRepository.Delete(entity.Id);
        }
    
    }
}
