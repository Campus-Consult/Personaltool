using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.Models
{
    public class ApplicationUser : IdentityUser
    {
        /* IdentityUser provide
         * Id, Username, Email, PhoneNumber, Password*/

        public Person Person { get; set; }
    }
}
