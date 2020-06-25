using Microsoft.AspNetCore.Mvc;
using Personaltool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.ViewModels.Person
{
    public class PersonsPositionHistoryViewModel
    {
        [HiddenInput]
        public int PersonsPositionID { get; set; }

        [HiddenInput]
        public int PersonID { get; set; }

        [HiddenInput]
        public int PositionID { get; set; }

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


        public PersonsPositionHistoryViewModel(PersonsPosition personsPosition)
        {
            this.PersonsPositionID = personsPosition.PersonPositionID;
            this.PersonID = personsPosition.PersonID;
            this.PositionID = personsPosition.PositionID;
            this.Name = personsPosition.Position.Name;
            this.ShortName = personsPosition.Position.ShortName;
            this.IsActive = personsPosition.Position.IsActive;
            this.Begin = personsPosition.Begin;
            this.End = personsPosition.End;
        }
    }
}
