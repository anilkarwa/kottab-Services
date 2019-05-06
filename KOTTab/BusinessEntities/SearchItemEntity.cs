using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KOTTab.BusinessEntities
{
    public class SearchItemEntity
    {
        public int itemId { get; set; }
        public String itemCode { get; set; }
        public  String itemName { get; set; }
        public String itemDisplayName { get; set; }
        public String itemDesc { get; set; }
        public int KCATID { get; set; }
        public float itemRate { get; set; }
        public int itemGroupID { get; set; }
        
    }
}