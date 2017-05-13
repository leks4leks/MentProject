using MentProject.Enums;
using MentProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MentProject.Helper
{
    public static class DateBaseSet
    {
        public static void FirstFill(MentProject.Models.ApplicationDbContext context)
        {
            _context = context;
            string[] roles = new string[] { RolesEnum.user.ToString(), RolesEnum.admin.ToString(), RolesEnum.aspadmin.ToString() };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                }
            }

            CreateUserByRole(RolesEnum.admin.ToString());
            CreateUserByRole(RolesEnum.aspadmin.ToString());
        }

        private static ApplicationDbContext _context;

        private static async void CreateUserByRole(string name)
        {
            var user = new ApplicationUser
            {
                UserName = name,
                Email = name + "@email.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher();
                var hashed = password.HashPassword(name);
                user.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, name);
            }

            await _context.SaveChangesAsync();
        }

    }
}