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
    /// <summary>
    /// PersonController, controlls person managing actions 
    /// </summary>
    [Authorize]
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Person/Index/
        public async Task<IActionResult> IndexAsync()
        {
            List<Person> persons = await _context.Persons.ToListAsync<Person>();
            List<PersonIndexViewModel> viewModels = persons.ConvertAll<PersonIndexViewModel>(person => new PersonIndexViewModel(person));
            viewModels.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));
            return View(viewModels);
        }

        // Partial view
        // GET: /Person/_PersonDetailsPartial/
        public async Task<IActionResult> PersonDetailsPartial(int id)
        {
            Person person = await _context.Persons.FindAsync(id);
            PersonDetailsViewModel viewModel = new PersonDetailsViewModel(person);

            return PartialView("_PersonDetailsPartial", viewModel);
        }
    }
}
