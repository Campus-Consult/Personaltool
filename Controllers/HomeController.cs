using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Personaltool.Models;
using Personaltool.Helpers;

namespace Personaltool.Controllers
{
    /// <summary>
    /// HomeController, controlls calls of base views like homepage, privacy declaration and lagal notice
    /// </summary>
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: /Home/Index/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Privacy/
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: /Home/LegalNotice/
        public IActionResult LegalNotice()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode = 500, String message = null)
        {
            return this.StatusCodeMessage(statusCode, message);
        }
    }
}
