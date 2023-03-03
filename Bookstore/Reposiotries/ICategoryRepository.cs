using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface ICategoryRepository
    {
        Task<List<Category>> getall();
        Task<Category> getbyid(int id);
        Task<Category> getbyname(string name);
        Task<Category> Add(Category category);
        Category update(Category category);
        public Category deletecategory(int id);
    }
}
