using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace LYSApp.Model
{
   public class User:IdentityUser<long,UserLogin,UserRole,UserClaim>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBackGroundVerified { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }
        public bool Status { get; set; }
        public int ManagerID { get; set; }
        public string ProfilePicture { get; set; }
        public Nullable<int> Gender { get; set; }


        public virtual ICollection<Bed> Beds { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager userManager)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

       
    }
}
