using Personaltool.Application.DataTransferObjects.Position.Common;
using System.Collections.Generic;

namespace Personaltool.Application.DataTransferObjects.Position.Queries
{
    public class PositionHistoryQueryDto
    {
        public PositionHistoryQueryDto()
        {
            CurrentAssignees = new HashSet<AssigneeDto>();
            FormerAssignees = new HashSet<AssigneeDto>();
            FutureAssignees = new HashSet<AssigneeDto>();
        }

        public int ID { get; set; }

        /// <summary>
        /// Current assingees
        /// </summary>
        public IEnumerable<AssigneeDto> CurrentAssignees { get; set; }

        /// <summary>
        /// Former assingees
        /// </summary>
        public IEnumerable<AssigneeDto> FormerAssignees { get; set; }

        /// <summary>
        /// Future assingees
        /// </summary>
        public IEnumerable<AssigneeDto> FutureAssignees { get; set; }

    }
}
