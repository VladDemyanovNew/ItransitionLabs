using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult Index(string socialNetwork, string status, SortState sortOrder = SortState.IdAsc)
        {
            List<User> users = _applicationDbContext.GetConfigUsers();

            FilterViewModel filterViewModel = Filter(ref users, socialNetwork, status);
            SortViewModel sortViewModel = Sort(ref users, sortOrder);

            IndexViewModel viewModel = new IndexViewModel
            {
                SortViewModel = sortViewModel,
                FilterViewModel = filterViewModel,
                Users = users
            };

            return View(viewModel);
        }

        private SortViewModel Sort(ref List<User> users, SortState sortOrder)
        {
            users = (sortOrder switch
            {
                SortState.IdDesc => users.OrderByDescending(s => s.Id),
                SortState.EmailAsc => users.OrderBy(s => s.Email),
                SortState.EmailDesc => users.OrderByDescending(s => s.Email),
                SortState.SocialNetworkAsc => users.OrderBy(s => s.ProviderDisplayName),
                SortState.SocialNetworkDesc => users.OrderByDescending(s => s.ProviderDisplayName),

                SortState.RegDateAsc => users.OrderBy(s => s.RegistrationDate),
                SortState.RegDateDesc => users.OrderByDescending(s => s.RegistrationDate),

                SortState.LoginDateAsc => users.OrderBy(s => s.LastLoginDate),
                SortState.LoginDateDesc => users.OrderByDescending(s => s.LastLoginDate),

                SortState.BlockedAsc => users.OrderBy(s => s.IsBlocked),
                SortState.BlockedDesc => users.OrderByDescending(s => s.IsBlocked),
                _ => users.OrderBy(s => s.Id),
            }).ToList();
            return new SortViewModel(sortOrder);
        }

        private FilterViewModel Filter(ref List<User> users, string socialNetwork, string status)
        {
            if (!String.IsNullOrEmpty(socialNetwork) && socialNetwork != "All")
                users = users.Where(user => user.ProviderDisplayName == socialNetwork).ToList();

            if (!String.IsNullOrEmpty(status) && status != "All")
                users = users.Where(user => user.IsBlocked.ToString() == status).ToList();

            List<string> socialNetworks = _applicationDbContext.UserLogins.Select(u => u.ProviderDisplayName).Distinct().ToList();

            return new FilterViewModel(socialNetworks, socialNetwork, status);
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

                    //blockedUser.LockoutEnd = DateTime.MaxValue;
                    blockedUser.LockoutEnd = DateTime.Now.AddYears(100);
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
