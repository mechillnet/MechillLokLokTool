using Microsoft.AspNetCore.Mvc.Rendering;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sSelectOption
{
    public interface ISelectOptionService
    {
        IPagedList<SelectOption> GetAll(string ClassificationCode, string name, int start = 0, int take = 15);
        SelectOption GetSelectOptionByCode(string Code, string ClassificationCode);
        SelectOption GetSelectOptionById(int Id);
        IList<SelectListItem> GetListSelectOption(string ClassificationCode);
        void Insert(SelectOption entity);
        void Update(SelectOption entity);
        void Delete(SelectOption entity);
    }
}
