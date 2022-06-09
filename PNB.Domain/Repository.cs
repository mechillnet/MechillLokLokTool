
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace PNB.Domain
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbPhim77 context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(DbPhim77 context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public T GetById(int id)
        {
            return entities.Find(id);
        }
        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Add(entity);
            context.SaveChanges();
        }
        public void BulkInsert(IList<T> entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            using (var transaction = new TransactionScope())
            {
                context.BulkInsert(entity);
                transaction.Complete();
            }

        }
        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            entities.Update(entity);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            if (id == 0) throw new ArgumentNullException("entity");

            T entity = entities.Find(id);
            entities.Remove(entity);
            context.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            entities.Remove(entity);
            context.SaveChanges();
        }
        public virtual void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.RemoveRange(entities);
                context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(exception.Message);
            }
        }
        public virtual IQueryable<T> Table => Entities;
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (entities == null)
                    entities = context.Set<T>();

                return entities;
            }
        }
        public void ExecuteSqlCommand(string sql)
        {
           
            context.Database.ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// Gets an entity set
        /// </summary>

    }
}
