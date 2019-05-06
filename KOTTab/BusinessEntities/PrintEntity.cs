using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KOTTab.BusinessEntities
{
    public class PrintEntity
    {
        public PrintData[] printData { get; set; }
        public String printerAddress { get; set; }
        public int waiterId { get; set; }
        public int kotNumber { get; set; }
        public int tableNumber { get; set; }
        public int PAX { get; set; }

    }
}