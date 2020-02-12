using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSend_DotNetCore.Models
{
    public class EmailConfig
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IFormFile Attachment { get; set; }
    }
    public class mailConfig
    {
        public string mailFrom { get; set; }
        public string mailpassword { get; set; }
    }
}
