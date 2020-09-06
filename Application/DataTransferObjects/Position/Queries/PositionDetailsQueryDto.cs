using Personaltool.Application.DataTransferObjects.Position.Common;
using System.Collections.Generic;

namespace Personaltool.Application.DataTransferObjects.Position.Queries
{
    /// <summary>
    /// Currently the same as the PersonDto but maybe later not more
    /// </summary>
    public class PositionDetailsQueryDto
    {
        public PositionDetailsQueryDto()
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
