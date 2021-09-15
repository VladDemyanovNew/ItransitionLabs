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
    }
}
