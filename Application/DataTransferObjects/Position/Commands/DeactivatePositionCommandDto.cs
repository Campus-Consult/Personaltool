using System;

namespace Personaltool.Application.DataTransferObjects.Position.Commands
{
    public class DeactivatePositionCommandDto
    {
        /// <summary>
        /// The postion that should be deactivated.
        /// </summary>
        public int PositionID { get; set; }

        /// <summary>
        /// The date of the deactivation.
        /// </summary>
        public DateTime TargetDate { get; set; }
    }
}
