using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.Models
{
    public class CareerLevel
    {
        [Key]
        public int CareerLevelID { get; set; }

        [Required]
        [Display(Name = "Bezeichung")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Kurzbezeichnung")]
        public string ShortName { get; set; }

        [Required]
        [Display(Name = "Aktiv")]
        public Boolean IsActive { get; set; }

        public ICollection<PersonsCareerLevel> PersonsCareerLevels { get; set; }
    }
}
