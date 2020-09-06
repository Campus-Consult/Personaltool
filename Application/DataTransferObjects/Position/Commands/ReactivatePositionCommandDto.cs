using System;
using System.Collections.Generic;

namespace Personaltool.Application.DataTransferObjects.Position.Commands
{
    public class ReactivatePositionCommandDto
    {
        /// <summary>
        /// The postion that should be reactivated.
        /// </summary>
        public int PositionID { get; set; }

        /// <summary>
        /// The date of the reactivation.
        /// </summary>
        public DateTime TargetDate { get; set; }

        /// <summary>
        /// The ids of the persons that should be assigned.
        /// </summary>
        public IEnumerable<int> PersonsAssigend { get; set; }
    }
}
