using System;

namespace Personaltool.Application.DataTransferObjects.Position.Common
{
    public class AssigneeDto
    {
        public int PersonID { get; set; }
        public string PersonName { get; set; }
        public DateTime Begin { get; set; }
        public DateTime? End { get; set; }
    }
}
