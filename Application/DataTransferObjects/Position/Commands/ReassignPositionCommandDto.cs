using System;
using System.Collections.Generic;

namespace Personaltool.Application.DataTransferObjects.Position.Commands
{
    public class ReassignPositionCommandDto
    {
        /// <summary>
        /// The ids of the persons that should be assigned.
        /// </summary>
        public IEnumerable<int> PersonsAssigned { get; set; }

        /// <summary>
        /// The ids of the persons that should be dismissed.
        /// </summary>
        public IEnumerable<int> PersonsDismissed { get; set; }

        /// <summary>
        /// The id of the position the persons should be assigned to.
        /// </summary>
        public int TargetPositionId { get; set; }

        /// <summary>
        /// The date of the assignment or dismissal.
        /// </summary>
        public DateTime TargetDate { get; set; }
    }
}
