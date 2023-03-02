using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface ICategoryRepository
    {
        Task<List<Category>> getall();
        Task<Category> getbyid(int id);
        Task<Category> getbyname(string name);
        Task Add(Category category);
        public Category update(int id, Category category);
        public Category deletecategory(int id);
    }
}
