using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KOTTab.BusinessEntities
{
    public class Printers
    {
        public int KCatId { get; set; }
        public String KCatName { get; set; }
        public int FoodAreaID { get; set; }
        public String FoodAreaName { get; set; }
        public String PrntPath { get; set; }
        public int PrntCopy { get; set; }
        public int Id { get; set; }
        public String PrntPath2 { get; set; }
        public int PrntCopy2 { get; set; }
        public String kotTitle { get; set; }
        public String kotTitle2 { get; set; }
    }
}