
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
            : base("LYSDemoEntities")
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
            modelBuilder.Entity<User>().ToTable("User").Property(p => p.Id).HasColumnName("UserID");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim").Property(p => p.Id).HasColumnName("UserClaimID");
            modelBuilder.Entity<Role>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleID"); ;
            
        }

        #endregion
    }
}