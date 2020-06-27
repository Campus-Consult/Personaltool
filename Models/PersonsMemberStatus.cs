using System;
using System.ComponentModel.DataAnnotations;
namespace Personaltool.Models
{
    public class PersonsMemberStatus
    {

        [Key]
        public int PersonsMemberStatusID { get; set; }

        [Required]
        public int PersonID { get; set; }

        [Required]
        public int MemberStatusID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Beginn")]
        public DateTime Begin { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ende")]
        public DateTime? End { get; set; }


        public Person Person { get; set; }


        public MemberStatus MemberStatus { get; set; } 


       
    }
}
