using Microsoft.AspNetCore.Mvc;
using Personaltool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.ViewModels.Person
{
    public class PersonIndexViewModel
    {
        [Required]
        [HiddenInput]
        public int PersonID { get; set; }

        [Required]
        [Display(Name = "Vorname")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nachname")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string EmailAssociaton { get; set; }


        public PersonIndexViewModel(Models.Person person)
        {
            this.PersonID = person.PersonID;
            this.FirstName = person.FirstName;
            this.LastName = person.LastName;
            this.EmailAssociaton = person.EmailAssociaton;
        }
    }
}
