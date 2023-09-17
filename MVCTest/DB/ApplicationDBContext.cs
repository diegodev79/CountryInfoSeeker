using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCTest.Models;

namespace MVCTest.DB
{  
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityUserRole<string>> UserRoles { get; set; }
        public DbSet<IdentityUserClaim<string>> UserClaims { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the ApplicationUser entity to use the "Users" table
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            // Configure the IdentityRole entity to use the "Roles" table
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            // Configure the IdentityUserClaim<string> entity to use the "UserClaims" table
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            // Configure the relationship between User and UserRole
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

           

            // Define foreign key relationships without attempting to access a non-existent User property
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasOne<IdentityRole>()
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            // Configure other entity mappings here
        }
       
    }

}
