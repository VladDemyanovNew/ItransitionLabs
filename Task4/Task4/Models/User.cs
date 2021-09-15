using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task4.Models
{
    public class User : IdentityUser
    {
        public DateTime? RegistrationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string ProviderDisplayName { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsChecked { get; set; }
    }
}
