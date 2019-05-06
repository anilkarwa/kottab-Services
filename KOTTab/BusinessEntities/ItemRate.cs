using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KOTTab.BusinessEntities
{
    public class ItemRate
    {
        public int itemId { get; set; }
        public int PCATID { get; set; }
        public decimal itemRate { get; set; }
        public decimal KOTQuantity { get; set; }
    }
}