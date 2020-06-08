using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.Models
{
    /// <summary>
    /// Person model, contains all personal data
    /// </summary>
    public class Person
    {
        [Key]
        public string PersonID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public string Gender { get; set; }

        public string EmailPrivate { get; set; }

        public string EmailAssociaton { get; set; }

        public string MobilePrivate { get; set; }

        public string AdressStreet { get; set; }

        public string AdressNr { get; set; }

        public string AdressZIP { get; set; }

        public string AdressCity { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
             
    }
}
