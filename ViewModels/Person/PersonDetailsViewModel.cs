using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.ViewModels.Person
{
    public class PersonDetailsViewModel
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

        [Display(Name = "Geburtstag")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Geschlecht")]
        public Gender Gender { get; set; }

        [Display(Name = "E-Mail (privat)")]
        [DataType(DataType.EmailAddress)]
        public string EmailPrivate { get; set; }

        [Display(Name = "E-Mail (Campus Consult)")]
        [DataType(DataType.EmailAddress)]
        public string EmailAssociaton { get; set; }

        [Display(Name = "Private Handynummer")]
        [DataType(DataType.PhoneNumber)]
        public string MobilePrivate { get; set; }

        [Display(Name = "Straße")]
        public string AdressStreet { get; set; }

        [Display(Name = "Hausnummer")]
        public string AdressNr { get; set; }

        [Display(Name = "PLZ")]
        [DataType(DataType.PostalCode)]
        public string AdressZIP { get; set; }

        [Display(Name = "Stadt")]
        public string AdressCity { get; set; }


        public PersonDetailsViewModel(Models.Person person)
        {
            this.PersonID = person.PersonID;
            this.FirstName = person.FirstName;
            this.LastName = person.LastName;
            this.Birthdate = person.Birthdate;
            this.Gender = person.Gender;
            this.EmailPrivate = person.EmailPrivate;
            this.EmailAssociaton = person.EmailAssociaton;
            this.MobilePrivate = person.MobilePrivate;
            this.AdressStreet = person.AdressStreet;
            this.AdressNr = person.AdressNr;
            this.AdressZIP = person.AdressZIP;
            this.AdressCity = person.AdressCity;
        }
    }
}
