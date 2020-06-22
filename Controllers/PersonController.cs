using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personaltool.Data;
using Personaltool.Models;
using Personaltool.ViewModels.Person;

namespace Personaltool.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            List<Person> persons = await _context.Persons.ToListAsync<Person>();
            List<PersonIndexViewModel> viewModels = persons.ConvertAll<PersonIndexViewModel>(person => new PersonIndexViewModel(person));
            viewModels.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));
            return View(viewModels);
        }

        public async Task<IActionResult> PersonDetailsPartial(int id)
        {
            Person person = await _context.Persons.FindAsync(id);
            PersonDetailsViewModel viewModel = new PersonDetailsViewModel(person);

            return PartialView("_PersonDetailsPartial", viewModel);
        }
    }
}
