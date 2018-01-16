using DDD.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvcLibrarie.Models
{
    public class MCarte
    {
        public string Id { get; set; }
        public string Nr { get; set; }
        public string titlu { get; set; }
        public string autor { get; set; }
        public string an { get; set; }
        public Stare stare1 { get; set; }
        public Stare stare2 { get; set; }
        public Gen_tip gent { get; set; }
        public Gen_continut genc { get; set; }
        public Utilizator utiliz { get; set; }

    }
}