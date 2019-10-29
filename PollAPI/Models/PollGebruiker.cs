using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class PollGebruiker
    {
        public int PollGebruikerID { get; set; }
        public int PollID { get; set; }
        public int GebruikerID { get; set; }
        public Poll Poll { get; set; }
        public Gebruiker Gebruiker { get; set; }
    }
}
