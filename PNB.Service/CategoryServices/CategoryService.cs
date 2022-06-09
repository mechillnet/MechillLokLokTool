using PNB.Domain;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Linq;

namespace PNB.Service.CategoryServices
{
    public partial class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _movieCategoryRepository;
        public CategoryService(IRepository<Category> movieCategoryRepository)
        {
            _movieCategoryRepository = movieCategoryRepository;
        }
        public IPagedList<Category> GetAll(string search, int start = 0, int take = 15)
        {
            var query = from h in _movieCategoryRepository.Table select h;
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Name.Contains(search));

            return new PagedList<Category>(query, start, take);
        }

        public Category GetById(int Id)
        {
            if (Id == 0)
                return null;
            return _movieCategoryRepository.GetById(Id);
        }
        public Category GetByLink(string Link)
        {
            var query = from h in _movieCategoryRepository.Table where h.Link == Link select h;
            return query.FirstOrDefault();
        }
        public void Insert(Category entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieCategoryRepository.Insert(entity);
        }
        public void Update(Category entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieCategoryRepository.Update(entity);
        }
        public void Delete(Category entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _movieCategoryRepository.Delete(entity.Id);
        }
    }
}
