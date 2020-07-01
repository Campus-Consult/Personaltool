using Microsoft.AspNetCore.Mvc;
using Personaltool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.ViewModels.Person
{
    public class PersonsCarreerLevelHistoryViewModel
    {
        [HiddenInput]
        public int PersonsCareerLevelID { get; set; }

        [HiddenInput]
        public int PersonID { get; set; }

        [HiddenInput]
        public int CareerLevelID { get; set; }

        [Display(Name = "Bezeichung")]
        public string Name { get; set; }

        [Display(Name = "Kurzbezeichnung")]
        public string ShortName { get; set; }

        [Display(Name = "Aktiv")]
        public Boolean IsActive { get; set; }

        [Display(Name = "Startdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Begin { get; set; }

        [Display(Name = "Enddatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? End { get; set; }


        public PersonsCarreerLevelHistoryViewModel(PersonsCareerLevel personsCareerLevel)
        {
            this.PersonsCareerLevelID = personsCareerLevel.PersonsCareerLevelID;
            this.PersonID = personsCareerLevel.PersonID;
            this.CareerLevelID = personsCareerLevel.CareerLevelID;
            this.Name = personsCareerLevel.CareerLevel.Name;
            this.ShortName = personsCareerLevel.CareerLevel.ShortName;
            this.IsActive = personsCareerLevel.CareerLevel.IsActive;
            this.Begin = personsCareerLevel.Begin;
            this.End = personsCareerLevel.End;
        }
    }
}
