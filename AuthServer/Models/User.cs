using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AuthServer.Models
{
    //Add model validation
    public class User
    {
        [Required]
        [RegularExpression(@"^([A-Za-z][A-Za-z0-9._]{5,14})$", ErrorMessage = "Invalid UserName,6-15 alphanumeric characters _ allowed no spaces and first alphabet")]
        public string Username { get; set; }

        [Required] 
        [RegularExpression(@"^([A-Za-z0-9]{6,15})$", ErrorMessage = "Invalid Password,6-15 alphanumeric characters start with any alphnumeric no special characters allowed")]
        public string Password { get; set; }

        [Required]
        //TBD: Add validation such that only two values are permitted.
        public string UserType { get; set; }
    }
}
