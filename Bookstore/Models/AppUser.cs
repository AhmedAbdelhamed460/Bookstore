using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class AppUser: IdentityUser
    {

        [MaxLength(50)]
        public string firstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        //[RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$"
        //     , ErrorMessage = "password must have 1 Uppercase, 1 lowercase , 1 number , 1 non alphanumeric and at least 6 characters")]
        //public string password { get; set; }

        //[RegularExpression("^01[0125][0-9]{8}$")]
        //public int phone { get; set; }


        //[EmailAddress]
        //public string Email { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }
       
        public virtual List<Order>? Order { get; set; }= new List<Order>();

        public virtual Review? Review { get; set; }
        public virtual ShopingCart? ShopingCart { get; set; }

    }
}
