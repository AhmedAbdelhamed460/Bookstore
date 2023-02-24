using Bookstore.DOT;
using Bookstore.Models;
using Bookstore.Responses;

namespace Bookstore.Interfaces
{
    public interface IAuthorService
    {
        Task SavePostImageAsync(AuthorBookDTO postRequest);
        Task<AuthorResponse> CreatePostAsync(AuthorBookDTO postRequest);
    }
}
