using Bookstore.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bookstore.DOT
{
    public class AuthorBookDTO
    {
        public int authorId { get; set; }
        public string Firstname { get; set; }

        public string Lastname { get; set; }
        public IFormFile ImageText { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string Image { get; set; }
        public string Bio { get; set; }
        public bool Top { get; set; }
        public List<string>bookName{ get; set; }=new List<string>();
    }
}
