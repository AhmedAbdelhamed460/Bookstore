using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IPublisherRepository
    {
        Task <List<Publisher>> GetAll();
        Task <Publisher> GetById(int id);
        Task<Publisher> getbyname(string name);
        Task Add(Publisher publisher);
   
        Publisher update(Publisher publisher);
        public Publisher deletePublisher(int id);


    }
}
