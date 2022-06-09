using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PNB.Service.HistoryLinkServices
{

    public partial class HistoryLinkService : IHistoryLinkService
    {
        private readonly IRepository<HistoryLink> _historyLinkRepository;
        public HistoryLinkService(IRepository<HistoryLink> historyLinkRepository)
        {
            _historyLinkRepository = historyLinkRepository;
        }


        public HistoryLink GetById(int Id)
        {
            if (Id == 0)
                return null;
            return _historyLinkRepository.GetById(Id);
        }
        public HistoryLink GetByLink(string Link, string Type)
        {
            return _historyLinkRepository.Table.Where(x => x.Type == Type && x.Link == Link).FirstOrDefault();
        }
      
        public void Insert(HistoryLink history)
        {
            if (history == null)
                throw new ArgumentNullException(nameof(history));

            _historyLinkRepository.Insert(history);
        }
        public void Update(HistoryLink history)
        {
            if (history == null)
                throw new ArgumentNullException(nameof(history));

            _historyLinkRepository.Update(history);
        }
        public void Delete(HistoryLink history)
        {
            if (history == null)
                throw new ArgumentNullException(nameof(history));

            _historyLinkRepository.Delete(history.Id);
        }
    }
}
