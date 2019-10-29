using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class Gebruiker
    {
        public int GebruikerID { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string Gebruikersnaam { get; set; }
        [NotMapped]
        public string Token { get; set; }

        public ICollection<PollGebruiker> PollGebruikers {get;set;}
        public ICollection<Stem> Stemmen { get; set; }
    }
}
