using System.ComponentModel.DataAnnotations;

namespace AuthServer.Models
{
    public class User
    {
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^([A-Za-z][A-Za-z0-9._]{5,14})$", ErrorMessage = "Invalid UserName,6-15 alphanumeric characters _ allowed no spaces and first alphabet")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression(@"^([A-Za-z0-9]{6,15})$", ErrorMessage = "Invalid Password,6-15 alphanumeric characters start with any alphnumeric no special characters allowed")]
        public string Password { get; set; }

        [Required(ErrorMessage = "UserType is Required")]
        [RegularExpression(@"^.*\b(AppUser|WebUser)\b.*$", ErrorMessage = "UserType Can Only be AppUser or WebUser")]
        public string UserType { get; set; }
    }
}
