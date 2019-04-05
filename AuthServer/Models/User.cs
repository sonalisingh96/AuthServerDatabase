namespace AuthServer.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        //TBD: Add validation such that only two values are permitted.
        public string UserType { get; set; }
    }
}
