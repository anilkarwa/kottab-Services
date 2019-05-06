using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KOTTab.BusinessEntities
{
    public class TableCountEntity
    {
        public int vacantTable { get; set; }
       public int occupiedTable { get; set; }
        public int paymentTable { get; set; }
    }
}