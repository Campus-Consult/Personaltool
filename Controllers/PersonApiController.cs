using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personaltool.Data;
using Personaltool.Models;
using Personaltool.ViewModels.Person;

namespace Personaltool.Controllers
{
    [Authorize]
    [Route("api/Person")]
    public class PersonApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Test() {
            return Json(new List<string>{"a", "b"});
        }

    }
}