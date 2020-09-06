using System.Collections.Generic;

namespace Personaltool.Application.DataTransferObjects.Position.Common
{
    public class PositionDto
    {
        public PositionDto()
        {
            CurrentAssignees = new HashSet<AssigneeDto>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Current assingees
        /// </summary>
        public IEnumerable<AssigneeDto> CurrentAssignees { get; set; }
    }
}
