using System.ComponentModel.DataAnnotations;

namespace Bookstore.DOT
{
    public class LoginDTO
    {
      //  [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        
    }
}
