using Microsoft.AspNetCore.Identity;
namespace Authentication.Contexts
{


    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
 
        }
    }
}