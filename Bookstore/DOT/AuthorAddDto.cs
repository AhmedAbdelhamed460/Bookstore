using System.ComponentModel.DataAnnotations;

namespace Bookstore.DOT
{
    public class AuthorAddDto
    {
       
        public string Firstname { get; set; }
       
        public string Lastname { get; set; }
       
        public string Image { get; set; }

        public string Bio { get; set; }
    }
}
