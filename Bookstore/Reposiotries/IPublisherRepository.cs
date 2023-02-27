using Bookstore.Models;

namespace Bookstore.Reposiotries
{
    public interface IPublisherRepository
    {
        public List<Publisher> GetAll();
        public Publisher GetById(int id);
        public Publisher getbyname(string name);

        public Publisher Add(Publisher publisher);
        public Publisher Update(int id,Publisher publisher);
        public Publisher deletePublisher(int id);


    }
}
