using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }


        //3 roles. user, admin, superadmin.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed roles.
            //List of roles.

            var adminRoleId = "123e4567-e89b-12d3-a456-426614174000";
            var superAdminRoleId = "123e4567-e89b-12d3-a456-426614164000";
            var userRoleId = "123e4567-e89b-12d3-a456-426614178000"; 

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                }, 
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin", 
                    Id = superAdminRoleId, 
                    ConcurrencyStamp = superAdminRoleId

                },
                 new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId

                }
            };

            //Seed superadmin.
            //EF will insert the roles in our database. 
            builder.Entity<IdentityRole>().HasData(roles);


            //Seed superadmin.
            var superAdminId = "123e4567-e89b-12d3-a456-426614171000";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper()
            };

            //password hash.
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "superadmin@123"); 

            builder.Entity<IdentityUser>().HasData(superAdminUser);


            //Add roles to superadmin.


            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId,
                },
                  new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId,
                },
                    new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles); 

        }
    }
}
