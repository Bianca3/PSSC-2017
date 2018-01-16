using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcLibrarie.Models
{
    public class MLibrarie
    {
        public Guid LibId { get; set; }
        public string Nume { get; set; }
        public int Utiliz { get; set; }
        public int Carti { get; set; }
    }
}