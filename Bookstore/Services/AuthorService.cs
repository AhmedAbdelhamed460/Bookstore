using Bookstore.DOT;
using Bookstore.Helpers;
using Bookstore.Interfaces;
using Bookstore.Models;
using Bookstore.Responses;
using DurableTask.Core.Common;

namespace Bookstore.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly BookStoreDbContext socialDbContext;
        private readonly IWebHostEnvironment environment;
        public AuthorService(BookStoreDbContext socialDbContext, IWebHostEnvironment environment)
        {
            this.socialDbContext = socialDbContext;
            this.environment = environment;
        }
        public async Task<AuthorResponse> CreatePostAsync(AuthorBookDTO postRequest)
        {
            var post = new Author
            {
                Firstname = postRequest.Firstname,
                Lastname = postRequest.Lastname,
                Bio = postRequest.Bio,
                Image = postRequest.Image,
                Top = false
                
            };
            var postEntry = await socialDbContext.Authors.AddAsync(post);
            var saveResponse = await socialDbContext.SaveChangesAsync();
            if (saveResponse < 0)
            {
                return new AuthorResponse { Success = false, Error = "Issue while saving the post", ErrorCode = "CP01" };
            }
            var postEntity = postEntry.Entity;
            var postModel = new AuthorBookDTO 
            {
                Firstname = postEntity.Firstname,
                Lastname = postEntity.Lastname,
                Bio = postEntity.Bio,
                Image = Path.Combine(postEntity.Image),
                Top = false
            };
            return new AuthorResponse { Success = true, author_ = postModel };

        }

        public async Task SavePostImageAsync(AuthorBookDTO postRequest)
        {
            var uniqueFileName = FileHelper.GetUniqueFileName(postRequest.ImageText.FileName);

            var uploads = Path.Combine(environment.WebRootPath, "authors", "images", postRequest.authorId.ToString());

            var filePath = Path.Combine(uploads, uniqueFileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            await postRequest.ImageText.CopyToAsync(new FileStream(filePath, FileMode.Create));

            postRequest.Image = filePath;
            return;
        }
    }
}
