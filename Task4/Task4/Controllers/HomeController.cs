using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Task4.Data;
using Task4.Models;

namespace Task4.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _applicationDbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDb)
        {
            _logger = logger;
            _applicationDbContext = applicationDb;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetSocialNetworks()
        {
            List<SocialNetwrok> socialNetworks = _applicationDbContext.UserLogins
                                        .GroupBy(user => user.ProviderDisplayName)
                                        .Select(item => new SocialNetwrok { ProviderDisplayName = item.Key, UserCount = item.Count() })
                                        .ToList();
            return Json(socialNetworks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
