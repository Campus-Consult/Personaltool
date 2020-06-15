using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.Models
{
    public class MemberStatus
    {
        [Key]
        public int MemberStatusID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public ICollection PersonsMemberStatus { get; set; }


    }
}
