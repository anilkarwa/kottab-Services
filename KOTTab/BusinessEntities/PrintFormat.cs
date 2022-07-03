using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KOTTab.BusinessEntities
{
    public class PrintFormat
    {

        public String companyName { get; set; }
        public String normalPrint { get; set; }
        public String rePrint { get; set; }
        public String cancelledPrint { get; set; }
        public String showCompanyName { get; set; }
        public int lineBreakOnTop { get; set; }
        public int lineBreakOnBottom { get; set; }

        public String counterPrint { get; set; }
        public String counterPrintPath { get; set; }
    }
}