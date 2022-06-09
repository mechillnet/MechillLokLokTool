using PNB.Domain.Infrastructure;
using PNB.Domain.Models;

namespace PNB.Service.CategoryServices
{
    public interface ICategoryService
    {
        IPagedList<Category> GetAll(string search, int start = 0, int take = 15);

        Category GetById(int Id);
        Category GetByLink(string Link);
        void Insert(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
