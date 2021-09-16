using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.Models;

namespace Task4.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<UserAuthDate> UserAuthDates { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public void CreateUserAuthDate(string id, DateTime date)
        {
            UserAuthDates.Add(new UserAuthDate() { Id = id, RegistrationDate = date });
            SaveChanges();
        }

        public async Task UpdateLoginDate(string id, DateTime date)
        {
            var user = await UserAuthDates.FirstAsync(u => u.Id == id);
            user.LastLoginDate = date;
            SaveChanges();
        }

        public void DeleteUserAuthDateById(string id)
        {
            var dateUser = UserAuthDates.First(d => d.Id == id);
            UserAuthDates.Remove(dateUser);
            SaveChanges();
        }

        public List<User> GetConfigUsers()
        {
            List<User> users = (from u in Users
                                join d in UserAuthDates on u.Id equals d.Id
                                join f in UserLogins on u.Id equals f.UserId
                                select
                                new User
                                {
                                    Id = u.Id,
                                    Email = u.Email,
                                    IsBlocked = u.LockoutEnd != null,
                                    LastLoginDate = d.LastLoginDate,
                                    RegistrationDate = d.RegistrationDate,
                                    ProviderDisplayName = f.ProviderDisplayName
                                }).ToList();
            return users;
        }
    }
}
