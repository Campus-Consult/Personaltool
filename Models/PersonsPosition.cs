using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.Models
{
    public class PersonsPosition
    {
        public int PersonPositionID { get; set; }

        public int PersonID { get; set; }

        public int PositionID { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public Person Person { get; set; }

        public Position Position { get; set; }
    }
}
