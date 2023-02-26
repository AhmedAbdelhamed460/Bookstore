using AutoMapper;
using Bookstore.DOT;
using Bookstore.Models;

namespace Bookstore.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
                CreateMap<Book,BookDetailsDto>();

        }
    }
}
