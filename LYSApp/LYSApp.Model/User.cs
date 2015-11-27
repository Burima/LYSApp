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
       public override long Id
       {
           get
           {
               return base.Id;
           }
           set
           {
               base.Id = value;
           }
       }

       public override string UserName
       {
           get
           {
               return base.UserName;
           }
           set
           {
               base.UserName = value;
           }
       }
       public override string PasswordHash
       {
           get
           {
               return base.PasswordHash;
           }
           set
           {
               base.PasswordHash = value;
           }
       }

       public override string Email
       {
           get
           {
               return base.Email;
           }
           set
           {
               base.Email = value;
           }
       }

       public override bool EmailConfirmed
       {
           get
           {
               return base.EmailConfirmed;
           }
           set
           {
               base.EmailConfirmed = value;
           }
       }

       public override string PhoneNumber
       {
           get
           {
               return base.PhoneNumber;
           }
           set
           {
               base.PhoneNumber = value;
           }
       }

       public override bool PhoneNumberConfirmed
       {
           get
           {
               return base.PhoneNumberConfirmed;
           }
           set
           {
               base.PhoneNumberConfirmed = value;
           }
       }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBackGroundVerified { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> LastUpdatedOn { get; set; }
        public string ProfilePicture { get; set; }
        public Nullable<int> Gender { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<Bed> Beds { get; set; }
        public virtual ICollection<PGReview> PGReviews { get; set; }
        public virtual ICollection<PGDetail> PGDetails { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager userManager)
        {
            var userIdentity = await userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

       
    }
}
