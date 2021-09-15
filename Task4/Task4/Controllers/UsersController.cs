using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task4.Data;
using Task4.Models;

namespace Task4.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        ApplicationDbContext _applicationDbContext;
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;

        public UsersController(ApplicationDbContext applicationDbContext, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("")]
        [Authorize]
        public IActionResult Index()
        {
            List<User> users = (from u in _applicationDbContext.Users
                                join d in _applicationDbContext.UserAuthDates on u.Id equals d.Id
                                join f in _applicationDbContext.UserLogins on u.Id equals f.UserId
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
            return View(users);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAsync(string[] selectedUsers)
        {
            if (selectedUsers.Length != 0)
            {
                foreach (string userId in selectedUsers)
                {
                    var deletedUser = await _userManager.FindByIdAsync(userId);
                    if (_userManager.GetUserId(HttpContext.User) != userId)
                        return Redirect("/Identity/Account/Login");

                    _applicationDbContext.DeleteUserAuthDateById(userId);
                    await _userManager.DeleteAsync(deletedUser);
                    await _signInManager.SignOutAsync();
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Users");
        }

        [HttpPost("Block")]
        public async Task<ActionResult> BlockAsync(string[] selectedUsers)
        {
            if (selectedUsers.Length != 0)
            {
                foreach (string userId in selectedUsers)
                {
                    var blockedUser = await _userManager.FindByIdAsync(userId);
                    if (_userManager.GetUserId(HttpContext.User) != userId)
                        return Redirect("/Identity/Account/Login");

                    blockedUser.LockoutEnd = DateTime.MaxValue;
                    await _userManager.UpdateSecurityStampAsync(blockedUser);
                    await _userManager.UpdateAsync(blockedUser);
                    await _signInManager.SignOutAsync();
                }
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Users");
        }

        [HttpPost("Unblock")]
        public async Task<IActionResult> UnblockAsync(string[] selectedUsers)
        {
            foreach (var userId in selectedUsers)
            {
                var unblockedUser = await _userManager.FindByIdAsync(userId);
                unblockedUser.LockoutEnd = null;
                await _userManager.UpdateAsync(unblockedUser);
            }
            return RedirectToAction("Index", "Users");
        }

    }
}
