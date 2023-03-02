using System.ComponentModel.DataAnnotations;

namespace Bookstore.DOT
{
    public class ResetPasswordDTO
    {
        public string UserId { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
