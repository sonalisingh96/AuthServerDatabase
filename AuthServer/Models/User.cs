using System.ComponentModel.DataAnnotations;

namespace AuthServer.Models
{

    //Add model validation
    public class User
    {
        [Required] //TBD: Add pattern validation
        public string Username { get; set; }

        [Required] //TBD: Add minimum num characters
        public string Password { get; set; }

        //TBD: Add validation such that only two values are permitted.
        public string UserType { get; set; }
    }
}
