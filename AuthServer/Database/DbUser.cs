using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthServer.Database
{
    public class DbUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        //TBD: Check the value allowed values(app-user, web-user)
        public string UserType { get; set; }
    }
}
