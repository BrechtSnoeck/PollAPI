using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class GebruikerDto
    {
        public int GebruikerID { get; set; }
        public string Email { get; set; }
        public string Gebruikersnaam { get; set; }
    }
}
