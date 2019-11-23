using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class PollDto
    {
        public string Naam { get; set; }
        public ICollection<Keuze> Opties { get; set; }
        public int MakerID { get; set; }
        public int[] VriendenIDs { get; set; }
    }

    public class Keuze
    {
        public string Optie { get; set; }
    }
}
