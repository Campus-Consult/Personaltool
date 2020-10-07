using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Personaltool.Controllers
{
  [AllowAnonymous]
  public class AccountController : Controller
  {
    public IActionResult Login()
    {
      if(!HttpContext.User.Identity.IsAuthenticated)
      {
        return Challenge(OpenIdConnectDefaults.AuthenticationScheme);
      }
      return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
      return new SignOutResult(new[]
      {
        // OpenIdConnectDefaults.AuthenticationScheme, // don't sign out of msgraph
        CookieAuthenticationDefaults.AuthenticationScheme
      }, new AuthenticationProperties {
        RedirectUri = "/Account/SignedOut",
      });
    }

    public IActionResult SignedOut() {
      return Ok();
    }

    public IActionResult AuthStatus() {
      if (User.Identity.IsAuthenticated) {
        var status = new AuthStatusData() {
          Authenticated = true,
          Claims = User.Claims.ToDictionary(c => c.Type, c => c.Value),
          Permissions = User.FindFirst(c => c.Type == "Permissions").Value.Split(",").ToList(),
        };
        return Json(status);
      } else {
        var status = new AuthStatusData() {
          Authenticated = false,
          Claims = new Dictionary<string, string>(),
        };
        return Json(status);
      }
    }

    public class AuthStatusData {
      public bool Authenticated { get; set; }
      public Dictionary<string, string> Claims { get; set; }
      public List<string> Permissions { get; set; }
    }
  }
}