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
        public List<Leaderboard> LeaderboardList { get; set; }
        public string FormattedStartDate
        {
            get
            {
                return StartDate.ToString("MM/dd/yyyy");
            }
        }
        public string FormattedEndDate
        {
            get
            {
                return EndDate.ToString("MM/dd/yyyy");
            }
        }
    }

    public class Leaderboard
    {
        public decimal NetWorth{ get; set; }
        public int GameID { get; set; }
        public int UserID { get; set; }
        public int UserGameID { get; set; }
        public int AssetsID { get; set; }
        public int PortfolioID { get; set; }
        public decimal QuantityHeld { get; set; }
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public decimal CurrentPrice { get; set; }
        public string UserName { get; set; }

        public decimal FinalNetworth { get; set; }
        public List<Asset> PlayersAssets { get; set; }
        public string PlayerStatus { get; set; }
        
    }
}
