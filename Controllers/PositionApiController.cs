using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                .Select(pos => new PositionDTO(pos, history)));
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
                return Json(new PositionDTO(position, true));
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreatePositionDTO position) {
            var p = await _context.Positions.AddAsync(position.toPosition());
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), new PositionDTO(p.Entity, false));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EditPositionDTO position) {
            Console.WriteLine($"{position.positionID}, {position.name}, {position.isActive}, {position.shortName}");
            if (position.positionID != id) {
                return BadRequest($"{id} doesn't equal {position.positionID}");
            }
            var p = await _context.Positions.FirstOrDefaultAsync(pos => pos.PositionID == position.positionID);
            if (p == null) {
                return NotFound();
            }
            position.editPosition(p);
            // _context.Entry(p).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

    }

    public class CreatePositionDTO {
        public string name { get; set; }
        public string shortName { get; set; }

        public Position toPosition() {
            return new Position() {
                Name = this.name,
                ShortName = this.shortName,
            };
        }
    }

    public class EditPositionDTO : CreatePositionDTO {
        public int positionID { get; set; }
        public bool isActive { get; set; }
        public void editPosition(Position p) {
            p.IsActive = this.isActive;
            p.Name = this.name;
            p.ShortName = this.shortName;
        }
    }

    public class PositionDTO {
        public int PositionID { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public bool IsActive { get; set; }

        public List<PositionHolderDTO> CurrentHolders { get; set; }

        // If history is false, only the current holders are included, otherwise all holders are inlcuded
        public PositionDTO(Position pos, bool history) {
            PositionID = pos.PositionID;
            Name = pos.Name;
            ShortName = pos.ShortName;
            IsActive = pos.IsActive;
            CurrentHolders = pos.PersonsPositions?
                .Where(pp => history || pp.End == null)
                .OrderBy(p => p.Begin)
                .Select(p => new PositionHolderDTO(p))
                .ToList() ?? new List<PositionHolderDTO>();
        }
    }

    public class PositionHolderDTO {
        public int PersonID { get; set; }

        public DateTime Begin { get; set; }

        public DateTime? End { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public PositionHolderDTO(PersonsPosition persPos) {
            PersonID = persPos.PersonID;
            Begin = persPos.Begin;
            End = persPos.End;
            FirstName = persPos.Person.FirstName;
            LastName = persPos.Person.LastName;
        }
    }
}