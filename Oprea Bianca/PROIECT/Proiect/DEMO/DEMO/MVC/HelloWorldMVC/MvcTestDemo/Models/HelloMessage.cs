using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcTestDemo.Models
{
    public class HelloMessage
    {

        [Required]
        public string Name { get; set; }

        [Editable(false)]
        public string Message { get; set; }
    }
}