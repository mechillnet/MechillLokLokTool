using PNB.Domain;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNB.Service.sTopic
{
   public partial class TopicService : ITopicService
    {

        private IRepository<Topic> _topicRepository;
   
        public TopicService(IRepository<Topic> topicRepository)
        {
            _topicRepository = topicRepository;
        }
        public IPagedList<Topic> GetAll(string title = "", int start = 0, int lenght = 15)
        {
            var query = _topicRepository.Table;
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }
           
            return new PagedList<Topic>(query, start, lenght);
        }
        public IEnumerable<Topic> GetTopicTopMenu()
        {
            var query = from t in _topicRepository.Table where t.IncludeInTopMenu == true && t.Published ==true orderby t.Order select t ;
 
            return query;
        }
        public Topic GetById(int Id)
        {
            if (Id == 0)
                return null;
            var topic = _topicRepository.GetById(Id);
            return topic;
        }

        public void Insert(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            _topicRepository.Insert(topic);

        }
        public void Update(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            _topicRepository.Update(topic);
        }
        public void Delete(Topic topic)
        {
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            _topicRepository.Delete(topic.Id);
        }
    }
}
