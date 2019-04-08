using System.Linq;
using System.Threading.Tasks;
using AuthServer.Database;
using AuthServer.ErrorHandling;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Service
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public DbUser GetUserOrNull(string username)
        { 
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            return user;
        }

        public async Task<string> RegisterUser(string userName,string password,string userType)
        {
            if (userType != "AppUser" && userType !="WebUser")
                throw new AppException(400, "Usertype can only be App User or Web User");
                var data = _context.Users.FirstOrDefault(p => p.Username == userName);
                if (data != null)
                {
                    throw new AppException(400, "The user already exists");
                }

                data = new DbUser
                {
                    Username = userName,
                    Password = password,
                    UserType = userType
                };
                _context.Users.Add(data);
                await _context.SaveChangesAsync();
                return data.Id.ToString();
            
        }

        public async Task DeleteUser(int userId)
        {
            var data = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (data == null) throw new AppException(404, "the user is not found");
            _context.Users.Remove(data);
            await _context.SaveChangesAsync();
            
        }

         public async Task UpdateUser(string userName, string password,string userType,int userId)
         {
            
            var data = await _context.Users.FirstAsync(x => x.Id == userId);
            if (data == null) throw new AppException(404,"User Not found");
            {
                //data.Username = userName;
                data.Password = password;
                //data.UserType = userType;
            }
            _context.Users.Update(data);
           await _context.SaveChangesAsync();
             


         }            
    }         
}

