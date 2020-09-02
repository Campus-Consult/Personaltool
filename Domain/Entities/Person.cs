using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Personaltool.Domain.Entities
{
    /// <summary>
    /// Person model; contains all personal data
    /// </summary>
    public class Person
    {
        [Key]
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
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Geschlecht")]
        public Gender Gender { get; set; }

        [Display(Name = "Private E-Mail-Adresse")]
        [DataType(DataType.EmailAddress)]
        public string EmailPrivate { get; set; }

        [Display(Name = "E-Mail-Adresse der Organisation")]
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

        //public ApplicationUser ApplicationUser { get; set; }
        //Exception: http://go.microsoft.com/fwlink/?LinkId=724062

        [Display(Name = "Mitgliedsstatus")]
        public ICollection<PersonsMemberStatus> PersonsMemberStatus { get; set; }

        [Display(Name = "Karrierelevel")]
        public ICollection<PersonsCareerLevel> PersonsCareerLevels { get; set; }

        [Display(Name = "Posten")]
        public ICollection<PersonsPosition> PersonsPositions { get; set; }

    }
}

public enum Gender
{
    [Display(Name = "männlich")]
    MALE,
    
    [Display(Name = "weiblich")]
    FEMALE,

    [Display(Name = "divers")]
    DIVERSE
}
