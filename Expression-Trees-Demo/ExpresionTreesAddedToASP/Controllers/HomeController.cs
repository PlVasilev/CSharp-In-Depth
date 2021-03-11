using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExpresionTreesAddedToASP.Models;
using ExpresionTreesAddedToASP.Extensions;

namespace ExpresionTreesAddedToASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return this.View();
            // return this.RedirectToAction(c => c.Index());
        }

        public IActionResult Privacy()
        {
            return this.RedirectToAction<AnotherController>(c => c.SomeAction(5, "metoo"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
