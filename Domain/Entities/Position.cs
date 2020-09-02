using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Personaltool.Domain.Entities
{
    public class Position
    {
        [Key]
        public int PositionID { get; set; }

        [Required]
        [Display(Name = "Bezeichnung")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Kurzbezeichnung")]
        public string ShortName { get; set; }

        [Required]
        [Display(Name = "Aktiv")]
        public Boolean IsActive { get; set; }

        public ICollection<PersonsPosition> PersonsPositions { get; set; }
    }
}
