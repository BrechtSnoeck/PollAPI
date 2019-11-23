using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class StemDto
    {
        public int GebruikerID { get; set; }
        public int PollGebruikerID { get; set; }
        public int[] AntwoordIDs { get; set; }
    }
}
