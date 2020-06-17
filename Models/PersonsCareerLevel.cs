using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.Models
{
    public class PersonsCareerLevel
    {
        [Key]
        public int PersonsCareerLevelID { get; set; }

        [Required]
        public int PersonID { get; set; }

        [Required]
        public int CareerLevelID { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public Person Person { get; set; }

        public CareerLevel CareerLevel { get; set; }
    }
}
