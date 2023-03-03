using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> GetAll();
        Task<Publisher> GetById(int id);
        Task<Publisher> getbyname(string name);
        Task<Publisher> Add(Publisher publisher);

        Publisher update(Publisher publisher);
        public Publisher deletePublisher(int id);


    }
}
