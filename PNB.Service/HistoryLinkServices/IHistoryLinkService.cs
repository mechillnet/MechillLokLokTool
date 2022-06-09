using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.HistoryLinkServices
{
    public interface IHistoryLinkService
    {
        HistoryLink GetById(int Id);
        HistoryLink GetByLink(string Link,string Type);
        void Insert(HistoryLink entity);
        void Update(HistoryLink entity);
        void Delete(HistoryLink entity);
    }
}
