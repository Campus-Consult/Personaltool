using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace Personaltool.Controllers
{
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
        RedirectUri = "/",
      });
    }
  }
}