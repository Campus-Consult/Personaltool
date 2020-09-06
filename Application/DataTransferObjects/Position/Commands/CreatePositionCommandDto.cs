using System.Collections.Generic;

namespace Personaltool.Application.DataTransferObjects.Position.Commands
{
    public class CreatePositionCommandDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// The ids of the persons that should be assigned.
        /// </summary>
        public IEnumerable<int> PersonsAdded { get; set; }
    }
}
