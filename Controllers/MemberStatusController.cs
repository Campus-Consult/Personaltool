using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Personaltool.Controllers
{
    public class MemberStatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
