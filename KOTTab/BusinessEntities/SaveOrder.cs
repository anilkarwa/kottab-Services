using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KOTTab.BusinessEntities
{
    public class SaveOrder
    {
        public int KOTNO { get; set; }
        public int TableId { get; set; }
        public int WaiterId { get; set; }
        public String AddedBy { get; set; }
        public String KOTDate { get; set; }
        public String TimeOfKOT { get; set; }
        public String AddedDateTime { get; set; }
        public int PAX { get; set; }
        public KOTOrderItems[] ItemsList { get; set;}
    }
}