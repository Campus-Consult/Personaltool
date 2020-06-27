using Microsoft.AspNetCore.Mvc;
using Personaltool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.ViewModels.Person
{
    public class PersonsMemberStatusHistoryViewModel
    {
        [HiddenInput]
        public int PersonsMemberStatusID { get; set; }

        [HiddenInput]
        public int PersonID { get; set; }

        [HiddenInput]
        public int MemberStatusID { get; set; }

        [Display(Name = "Bezeichung")]
        public string Name { get; set; }

        [Display(Name = "Startdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Begin { get; set; }

        [Display(Name = "Enddatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? End { get; set; }


        public PersonsMemberStatusHistoryViewModel(PersonsMemberStatus personsMemberStatus)
        {
            this.PersonsMemberStatusID = personsMemberStatus.PersonsMemberStatusID;
            this.PersonID = personsMemberStatus.PersonID;
            this.MemberStatusID = personsMemberStatus.MemberStatusID;
            this.Name = personsMemberStatus.MemberStatus.Name;
            this.Begin = personsMemberStatus.Begin;
            this.End = personsMemberStatus.End;
        }
    }
}
