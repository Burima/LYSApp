using Microsoft.AspNet.Identity.EntityFramework;

namespace LYSApp.Model
{
    public class UserClaim : IdentityUserClaim<long>
    {

        public virtual User User { get; set; }
    }
}
