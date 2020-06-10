using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Personaltool.Models;
namespace Personaltool.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("/Index");
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/Index");
        }
    }
}
