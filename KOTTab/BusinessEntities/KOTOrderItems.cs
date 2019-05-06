using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KOTTab.BusinessEntities
{
    public class KOTOrderItems
    {
        public int KCATID { get; set; }
        public int KOTNO { get; set; }
        public int ItemId { get; set; }
        public String ItemName { get; set; }
        public int SlNo { get; set; }
        public int KOTQuantity { get; set; }
        public int KOTRate { get; set; }
        public int KOTAmount { get; set; }
        public String AdditionalInstructions { get; set; }

    }
}