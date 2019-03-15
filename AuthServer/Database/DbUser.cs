using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthServer.Database
{
    public class DbUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]//TBD: Is this supposed to be unique constraint
        public string Username { get; set; }

        public string Password { get; set; }
        public string UserType { get; set; }
    }
}
