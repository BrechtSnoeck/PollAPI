using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class PollGebruikerDto
    {
        public int PollGebruikerID { get; set; }
        public int PollID { get; set; }
        public int GebruikerID { get; set; }
        public bool Gestemd { get; set; }
        public bool IsAdmin { get; set; }
        public string Gebruikersnaam { get; set; }
        public Poll Poll { get; set; }
    }
}
