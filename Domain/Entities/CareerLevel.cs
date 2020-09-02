using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Personaltool.Domain.Entities
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
