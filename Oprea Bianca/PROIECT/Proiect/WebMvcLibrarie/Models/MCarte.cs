using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcLibrarie.Models
{
    public class MCarte
    {
        public Guid Id { get; set; }
        public string ISSN { get; set; }
        public string titlu { get; set; }
        public string autor { get; set; }
    }
}