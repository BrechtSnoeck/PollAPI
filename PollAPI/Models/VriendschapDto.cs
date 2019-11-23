using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class VriendschapDto
    {
        public int RelatieID { get; set; }
        public string GebruikersnaamZender { get; set; }
        public int GebruikersIDZender { get; set; }
        public string GebruikersnaamOntvanger { get; set; }
        public int GebruikersIDOntvanger { get; set; }
        public bool status { get; set; }
    }
}
