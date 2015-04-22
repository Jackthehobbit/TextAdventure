using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Room
    {
        public string gonorth { get; set; }
        public string gosouth { get; set; }
        public string goeast { get; set; }
        public string gowest { get; set; }
        public string description { get; set; }
        public int[] roomIds { get; set; }


    }
}
