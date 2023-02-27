using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface ICategoryRepository
    {
        public List<Category> getall();
        public Category getbyid(int id);
        public Category getbyname(string name);

        public Category Add(Category category);
        public Category update(int id, Category category);
        public Category deletecategory(int id);
    }
}
