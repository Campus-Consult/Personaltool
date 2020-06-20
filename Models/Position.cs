using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.Models
{
    public class Position
    {
        public int PositionID { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public Boolean IsActive { get; set; }

        public ICollection<Position> PersonsPositions { get; set; }
    }
}
