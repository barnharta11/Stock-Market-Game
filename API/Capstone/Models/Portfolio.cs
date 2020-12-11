using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Portfolio
    {
        public int UserGameID { get; set; }
        public decimal Balance { get; set; }
        public int QuantityHeld { get; set; }
        public string StockTicker { get; set; }
        public string CompanyName { get; set; }
        public decimal StockPrice { get; set; }
        public decimal PurchasedPrice { get; set; }
        public DateTime Time { get; set; }


    }

    
}
