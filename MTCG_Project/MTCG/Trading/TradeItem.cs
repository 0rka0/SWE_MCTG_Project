using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.MTCG.Trading
{
    public class TradeItem
    {
        public string id { get; set; }
        public string cardToTrade { get; set; }
        public string type { get; set; }
        public float minimumDamage { get; set; }
    }
}
