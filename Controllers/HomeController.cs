using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Personaltool.Helpers;
using Personaltool.Models;
using System.Security.Claims;

namespace Personaltool.Controllers
{
    /// <summary>
    /// HomeController, controlls calls of base views like homepage, privacy declaration and lagal notice
    /// </summary>
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthenticationService _authService;

        public HomeController(ILogger<HomeController> logger, IAuthenticationService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        // GET: /Home/Index/
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) {
                // ms Graph example
                // var client = await HttpContext.GetGraphServiceClient();
                // var user = await client.Me.Request().GetAsync();
                // _logger.LogDebug("EMail: "+mail);
            }
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
