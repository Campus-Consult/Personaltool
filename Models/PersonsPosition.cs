using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.Models
{
    public class PersonsPosition
    {
        [Key]
        public int PersonPositionID { get; set; }

        [Required]
        public int PersonID { get; set; }

        [Required]
        public int PositionID { get; set; }

        [Required]
        [Display(Name = "Startdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Begin { get; set; }

        [Display(Name = "Enddatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }

        [Required]
        public Person Person { get; set; }

        [Required]
        public Position Position { get; set; }
    }
}
