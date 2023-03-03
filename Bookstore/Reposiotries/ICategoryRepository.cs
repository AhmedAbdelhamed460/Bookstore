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

        public List<Category> getall();
        public Category getbyid(int id);
        public Category getbyname(string name);

        public Category Add(Category category);
        public Category update(int id, Category category);

        public Category deletecategory(int id);
    }
}
