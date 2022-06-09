using Microsoft.AspNetCore.Mvc.Rendering;
using PNB.Domain;
using PNB.Domain.Common;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PNB.Service.sSelectOption
{
   public partial class SelectOptionService : ISelectOptionService
    {
        private IRepository<SelectOption> _selectListOptionRepository;

        public SelectOptionService(IRepository<SelectOption> selectListOptionRepository)
        {
            _selectListOptionRepository = selectListOptionRepository;
        }
        public IPagedList<SelectOption> GetAll(string ClassificationCode,string name , int start = 0, int lenght = 15)
        {
            var query = from s in _selectListOptionRepository.Table where s.ClassificationCode == ClassificationCode  select s;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }
            query = query.OrderBy(x => x.Order);
            return new PagedList<SelectOption>(query, start, lenght);
        }
        public  SelectOption GetSelectOptionByCode(string Code, string ClassificationCode)
        {
            var query = from s in _selectListOptionRepository.Table where s.ClassificationCode == ClassificationCode && s.Code == Code select s;

            return query.FirstOrDefault();
        }
        public SelectOption GetSelectOptionById(int Id)
        {
            if (Id == 0)
                return null;
            var selectoption = _selectListOptionRepository.GetById(Id);
            return selectoption;
        }
        public IList<SelectListItem> GetListSelectOption(string ClassificationCode)
        {
            if (string.IsNullOrEmpty(ClassificationCode))
                return null;
            var listquery = from s in _selectListOptionRepository.Table where s.ClassificationCode == ClassificationCode && s.IsActive select new SelectListItem {Text = s.Name, Value = s.Code };
            return listquery.ToList();
        }

        public void Insert(SelectOption entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var queryLast = from p in _selectListOptionRepository.Table where p.ClassificationCode == entity.ClassificationCode orderby p.Code descending select p;
            var Last = queryLast.FirstOrDefault();
            int lastDigit = 1;
            string Prefix = "";
            switch (entity.ClassificationCode)
            {
                case ClassificationCode.CommodityType:
                    Prefix = "Comm";
                    break;
                case ClassificationCode.StatusOrder:
                    Prefix = "OrdSTT";
                    break;
                case ClassificationCode.StatusPayment:
                    Prefix = "PaySTT";
                    break;
                default:
                    break;
            }
            if (Last != null)
            {
                string Digit = Last.Code.Substring(Prefix.Length);
                lastDigit = Int32.Parse(Digit) + 1;
            }
          
            entity.Code = Prefix + lastDigit.ToString("D3");
            _selectListOptionRepository.Insert(entity);

        }
        public void Update(SelectOption entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _selectListOptionRepository.Update(entity);
        }
        public void Delete(SelectOption entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _selectListOptionRepository.Delete(entity.Id);
        }
 
    }
}
