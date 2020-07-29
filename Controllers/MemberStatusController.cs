using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personaltool.Data;
using Personaltool.Models;

namespace Personaltool.Controllers
{
    public class MemberStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Person/Index/
        public IActionResult Index()
        {
            var memberStatus = _context.MemberStatus.
                Include(x => x.PersonsMemberStatus);
            return View(memberStatus);
        }
    }
}
