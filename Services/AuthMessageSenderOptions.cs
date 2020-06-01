using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personaltool.Services
{
    public class AuthMessageSenderOptions
    {
        public string AuthMessageSenderOptions_Domain { get; set; }
        public string AuthMessageSenderOptions_UserName { get; set; }
        public string AuthMessageSenderOptions_SentFrom { get; set; }
        public string AuthMessageSenderOptions_Password { get; set; }
        public string AuthMessageSenderOptions_SMTPClient { get; set; }
    }
}
