using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class Poll
    {
        public int PollID { get; set; }
        public string Naam { get; set; }

        public ICollection<PollGebruiker> PollsVanGebruikers { get; set; }
        public ICollection<Antwoord> Antwoorden { get; set; }
    }
}
