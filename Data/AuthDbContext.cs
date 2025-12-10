using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Bloggie.web.Data
{

    public class AuthDbContext : IdentityDbContext

    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Seed Roles (User, Admin, SuperAdmin)
            var adminRoleId = "9A82FCFD-6778-463D-810B-80A7BFC01735";
            var superAdminRoleId = "d569da19-31b1-4983-897e-f2732f807119";
            var userRoleId = "91014637-1942-44b7-8a7b-a700f734e5e5";

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
                    Name ="SuperAdmin",
                    NormalizedName =" SuperAdmin",
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

            //EF core vil insert all these roles to database.
            builder.Entity<IdentityRole>().HasData(roles);


            //Seed SuperAdmin
            var superAdminId = "7f3e7cf7-8510-4e62-b772-e81dc008a57e";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@Bloggie.com",
                Email = "superadmin@bloggie.com".ToUpper(),
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                Id = superAdminId,
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "nargesadmin");
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add all de roles to super admin

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                 new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                  new IdentityUserRole<string>
                {
                    RoleId =userRoleId ,
                    UserId = superAdminId
                },
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
        
    }
}
