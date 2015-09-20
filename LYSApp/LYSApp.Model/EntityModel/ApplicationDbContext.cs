
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// A basic implementation for an application database context compatible with ASP.NET Identity 2 using
    /// <see cref="long"/> as the key-column-type for all entities.
    /// </summary>
    /// <remarks>
    /// This type depends on some other types out of this assembly.
    /// </remarks>
    /// 
namespace LYSApp.Model
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, long, UserLogin, UserRole, UserClaim>
    {
        #region constructors and destructors

        public ApplicationDbContext()
            : base("LYSAdminEntities")
        {
        }

        #endregion

        #region methods

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Map Entities to their tables.
            modelBuilder.Entity<User>().ToTable("Users").Property(p => p.Id).HasColumnName("UserID");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims").Property(p => p.Id).HasColumnName("UserClaimID");
            modelBuilder.Entity<Role>().ToTable("Roles").Property(p => p.Id).HasColumnName("RoleID"); ;
            
        }

        #endregion
    }
}