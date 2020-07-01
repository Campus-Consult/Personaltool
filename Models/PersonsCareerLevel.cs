using Microsoft.AspNetCore.Mvc;
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

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Startdatum")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Begin { get; set; }

        [Display(Name = "Enddatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? End { get; set; }

        [Required]
        public Person Person { get; set; }

        [Required]
        [Display(Name = "Karrierelevel")]
        public CareerLevel CareerLevel { get; set; }
    }
}
