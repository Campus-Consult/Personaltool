using Microsoft.EntityFrameworkCore;
using Personaltool.Application.DataTransferObjects.Position.Commands;
using Personaltool.Application.DataTransferObjects.Position.Common;
using Personaltool.Application.DataTransferObjects.Position.Queries;
using Personaltool.Application.Interfaces;
using Personaltool.Data;
using Personaltool.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Personaltool.Application.Services
{
    public class PositionService : IPositionService
    {
        private readonly ApplicationDbContext _context;

        public PositionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Queries
        public PositionOverviewQueryDto GetOverview()
        {
            var dto = new PositionOverviewQueryDto();

            var positions = _context.Positions
                .Where(x => x.IsActive == true)
                .Include(x => x.PersonsPositions
                    .Where(y => y.Begin <= DateTime.Now && y.End > DateTime.Now))
                .ThenInclude(z => z.Person);
            
            foreach (var p in positions)
            {
                var assignees = new HashSet<AssigneeDto>();

                foreach(var pp in p.PersonsPositions)
                {
                    assignees.Append(new AssigneeDto
                    {
                        PersonID = pp.PersonID,
                        PersonName = $"{pp.Person.FirstName} {pp.Person.LastName}",
                        Begin = pp.Begin,
                        End = pp.End,
                    });
                }

                dto.Positions.Append(new PositionDto
                {
                    ID = p.PositionID,
                    Name = p.Name,
                    ShortName = p.ShortName,
                    IsActive = p.IsActive,

                    CurrentAssignees = assignees,
                });
            }

            return dto;
        }

        public PositionListQueryDto GetList()
        {
            var dto = new PositionListQueryDto();

            var positions = _context.Positions
                .Where(x => x.IsActive == true)
                .Include(x => x.PersonsPositions
                    .Where(y => y.Begin <= DateTime.Now && y.End > DateTime.Now))
                .ThenInclude(z => z.Person);

            foreach (var position in positions)
            {
                var assignees = new HashSet<AssigneeDto>();

                foreach (var assignement in position.PersonsPositions)
                {
                    assignees.Append(new AssigneeDto
                    {
                        PersonID = assignement.PersonID,
                        PersonName = $"{assignement.Person.FirstName} {assignement.Person.LastName}",
                        Begin = assignement.Begin,
                        End = assignement.End,
                    });
                }

                dto.Positions.Append(new PositionDto
                {
                    ID = position.PositionID,
                    Name = position.Name,
                    ShortName = position.ShortName,
                    IsActive = position.IsActive,

                    CurrentAssignees = assignees,
                });
            }

            return dto;
        }

        public PositionDetailsQueryDto GetDetails(int id)
        {
            var position = _context.Positions
                .Where(x => x.IsActive == true)
                .Include(x => x.PersonsPositions
                    .Where(y => y.Begin <= DateTime.Now && y.End > DateTime.Now))
                .ThenInclude(z => z.Person)
                .First(x => x.PositionID == id);

            if (position is null)
            {
                return null;
            }

            var assignees = new HashSet<AssigneeDto>();

            foreach (var assignment in position.PersonsPositions)
            {
                assignees.Append(new AssigneeDto
                {
                    PersonID = assignment.PersonID,
                    PersonName = $"{assignment.Person.FirstName} {assignment.Person.LastName}",
                    Begin = assignment.Begin,
                    End = assignment.End,
                });
            }

            var dto = new PositionDetailsQueryDto
            {
                ID = position.PositionID,
                Name = position.Name,
                ShortName = position.ShortName,
                IsActive = position.IsActive,
                CurrentAssignees = assignees,
            };

            return dto;
        }

        public PositionHistoryQueryDto GetHistory(int id)
        {
            var position = _context.Positions
                .Where(x => x.IsActive == true)
                .Include(x => x.PersonsPositions)
                .ThenInclude(z => z.Person)
                .First(x => x.PositionID == id);

            var dto = new PositionHistoryQueryDto()
            {
                ID = id,
            };

            foreach(var assignment in position.PersonsPositions)
            {
                var assignementDto = new AssigneeDto
                {
                    PersonID = assignment.PersonID,
                    PersonName = $"{assignment.Person.FirstName} {assignment.Person.LastName}",
                    Begin = assignment.Begin,
                    End = assignment.End,
                };

                if (assignementDto.End != null && assignementDto.End <= DateTime.Now)
                {
                    dto.FormerAssignees.Append(assignementDto);
                    break;
                } else if (assignementDto.Begin <= DateTime.Now)
                {
                    dto.CurrentAssignees.Append(assignementDto);
                    break;
                } else
                {
                    dto.FutureAssignees.Append(assignementDto);
                    break;
                }
            }
            return dto;
        }

        // Commands
        public PositionDetailsQueryDto Create(CreatePositionCommandDto dto)
        {
            var position = new Position()
            {
                Name = dto.Name,
                ShortName = dto.ShortName,
                IsActive = dto.IsActive
            };

            var newPosition = _context.Positions.Add(position);

            var newDto = new PositionDetailsQueryDto
            {
                ID = position.PositionID,
                Name = position.Name,
                ShortName = position.ShortName,
                IsActive = position.IsActive,
            };

            return newDto;

        }
        public void Edit(EditPositionCommandDto dto)
        {
            var position = _context.Positions.Find(dto.ID);

            position.Name = dto.Name;
            position.ShortName = dto.ShortName;
            position.IsActive = dto.IsActive;

            _context.SaveChanges();
        }

        public void Deactivate(DeactivatePositionCommandDto dto)
        {
            Deactivate(dto.PositionID, dto.TargetDate);
        }

        public void Reactivate(ReactivatePositionCommandDto dto)
        {
            Reactivate(dto.PositionID);

            foreach (var PersonID in dto.PersonsAssigend)
            {
                Assign(PersonID, dto.PositionID, dto.TargetDate);
            }
        }

        public void Assign(AssignPositionCommandDto dto)
        {
            foreach (var TargetPersonId in dto.PersonsAssigned)
            {
                Assign(TargetPersonId, dto.TargetPositionId, dto.TargetDate);
            }
        }

        public void Dismiss(DismissPositionCommandDto dto)
        {
            foreach (var TargetPersonId in dto.PersonsDismissed)
            {
                Dismiss(TargetPersonId, dto.TargetPositionId, dto.TargetDate);
            }
        }

        public void Reassign(ReassignPositionCommandDto dto)
        {
            foreach (var TargetPersonId in dto.PersonsAssigned)
            {
                Assign(TargetPersonId, dto.TargetPositionId, dto.TargetDate);
            }
            foreach (var TargetPersonId in dto.PersonsDismissed)
            {
                Dismiss(TargetPersonId, dto.TargetPositionId, dto.TargetDate);
            }
        }

        // Helper
        private void Deactivate(int positionId, DateTime deactivation)
        {
            var position = _context.Positions.Include(x => x.PersonsPositions).First(x => x.PositionID == positionId);

            if (position == null)
            {
                return;
            }

            position.IsActive = false;
            _context.SaveChanges();

            foreach(var assignement in position.PersonsPositions)
            {
                Dismiss(assignement.PersonID, positionId, deactivation);
            }
        }

        private void Reactivate(int positionId)
        {
            var position = _context.Positions.Include(x => x.PersonsPositions).First(x => x.PositionID == positionId);

            if (position == null)
            {
                return;
            }

            position.IsActive = true;
            _context.SaveChanges();
        }

        private void Assign(int personId, int positionId, DateTime begin)
        {
            var assignement = _context.PersonsPositions.First(x => x.PersonID == personId && x.PositionID == positionId && x.End == null);

            if (assignement != null)
            {
                return;
            }

            _context.PersonsPositions.Add(new PersonsPosition
            {
                PersonID = personId,
                PositionID = positionId,
                Begin = begin,
            });
        }

        private void Dismiss(int personId, int positionId, DateTime end)
        {
            var assignment = _context.PersonsPositions.First(x => x.PersonID == personId && x.PositionID == positionId && x.End == null);

            if (assignment == null)
            {
                return;
            }

            assignment.End = end;

            _context.SaveChanges();
        }

    }
}
