using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Portfolio
    {
        int UserGameID { get; set; }
        List<Asset> PortfolioAssets { get; set; }
    }
}
