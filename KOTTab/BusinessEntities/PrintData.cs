using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KOTTab.BusinessEntities
{
    public class PrintData
    {
        public String ItemName { get; set; }
        public decimal KOTQuantity { get; set; }
        public String AdditionalInstructions { get; set; }
        public int KOTCATID { get; set; }

    }
}