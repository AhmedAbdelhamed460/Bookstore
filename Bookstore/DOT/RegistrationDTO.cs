using System.ComponentModel.DataAnnotations;

namespace Bookstore.DOT
{
    public class RegistrationDTO
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [RegularExpression("^01[0125][0-9]{8}$")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
