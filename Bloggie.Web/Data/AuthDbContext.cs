using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }


        //3 roles. user, admin, superadmin.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed roles.
            //List of roles.

            var adminRoleId = "c0bd43a5-4c5f-44e7-9f8c-02a126d945d8";
            var superAdminRoleId = "34085b0e-82f2-42e9-ba0b-bbab324e7a39";
            var userRoleId = "6e8008e2-48e0-426f-bd41-02f0df118863"; 

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
            var superAdminId = "dbfd35c1-fb32-4209-84e1-f6772b5c713f";
            var superAdminUser = new IdentityUser
            {
                Id = superAdminId, 
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(), 
               
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
