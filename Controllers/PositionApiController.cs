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
    //[Authorize]
    [Route("api/Position")]
    public class PositionApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PositionApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("")]
        public IActionResult List(bool history = false) {
            return Json(_context.Positions
                .Include(p => p.PersonsPositions)
                    .ThenInclude(pp => pp.Person)
                .Select(pos => PositionDTO.toDTO(pos, history)));
        }

        [Route("{id}")]
        public async Task<IActionResult> Get(int id) {
            var position = await _context.Positions
                .Include(p => p.PersonsPositions)
                    .ThenInclude(pp => pp.Person)
                .FirstOrDefaultAsync(p => p.PositionID == id);
            if (position == null) {
                return NotFound();
            } else {
                return Json(PositionDTO.toDTO(position, true));
            }
        }

    }

    public class PositionDTO {
        public int PositionID { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public bool IsActive { get; set; }

        public List<PositionHolderDTO> CurrentHolders { get; set; }

        // If history is false, only the current holders are included, otherwise all holders are inlcuded
        public static PositionDTO toDTO(Position pos, bool history) {
            return new PositionDTO() {
                PositionID = pos.PositionID,
                Name = pos.Name,
                ShortName = pos.ShortName,
                IsActive = pos.IsActive,
                CurrentHolders = pos.PersonsPositions
                    .Where(pp => history || pp.End == null)
                    .OrderBy(p => p.Begin)
                    .Select(p => PositionHolderDTO.toDTO(p))
                    .ToList(),
            };
        }
    }

    public class PositionHolderDTO {
        public int PersonID { get; set; }

        public DateTime Begin { get; set; }

        public DateTime? End { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public static PositionHolderDTO toDTO(PersonsPosition persPos) {
            return new PositionHolderDTO() {
                PersonID = persPos.PersonID,
                Begin = persPos.Begin,
                End = persPos.End,
                FirstName = persPos.Person.FirstName,
                LastName = persPos.Person.LastName,
            };
        }
    }
}