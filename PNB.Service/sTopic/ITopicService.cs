using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sTopic
{
    public  interface ITopicService
    {
        IPagedList<Topic> GetAll( string title = "", int start = 0, int lenght = 15);
        Topic GetById(int Id);
        IEnumerable<Topic> GetTopicTopMenu();
        void Insert(Topic user);
        void Update(Topic user);
        void Delete(Topic user);
    }
}
