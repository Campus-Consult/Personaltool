using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Personaltool.Domain.Entities
{
    public class MemberStatus
    {
        [Key]
        public int MemberStatusID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public ICollection<PersonsMemberStatus> PersonsMemberStatus { get; set; }


    }
}
