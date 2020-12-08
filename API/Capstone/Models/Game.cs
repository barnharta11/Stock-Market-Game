using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int CreatorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int WinnerId { get; set; }
        public string CreatorName { get; set; }
        public string StatusName { get; set; }


    }
}
