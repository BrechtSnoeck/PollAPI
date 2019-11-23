using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class Relatie
    {
        public int RelatieID { get; set; }
        public int GebruikerID_1 { get; set; }
        public int GebruikerID_2 { get; set; }
        public bool status { get; set; }
        public Gebruiker Zender { get; set; }
        public Gebruiker Ontvanger { get; set; }
    }
}
