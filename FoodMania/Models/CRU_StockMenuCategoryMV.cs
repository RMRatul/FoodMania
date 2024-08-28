using Dblayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodMania.Models
{
    public class CRU_StockMenuCategoryMV
    {
        FoodManiaEntities db = new FoodManiaEntities();

        public CRU_StockMenuCategoryMV()
        {
            GetData();
        }
        public CRU_StockMenuCategoryMV(int?id)
        {
            GetData();
            var edit = db.StockMenuCategoryTables.Find(id);
            if (edit != null)
            {
              StockMenuCategoryID = edit.StockMenuCategoryID;
                StockMenuCategory=edit.StockMenuCategory;
                CreatedBy_User=edit.CreatedBy_User;
            }
            else
            {
                StockMenuCategoryID = 0;
                StockMenuCategory = string.Empty;
                CreatedBy_User = 0;
            }
        }
        public int StockMenuCategoryID { get; set; }
        public string StockMenuCategory { get; set; }

        public int CreatedBy_User { get; set; }

        public virtual List<StockMenuCategoryMV> Lists { get; set; }

        private void GetData()
        {
            Lists= new List<StockMenuCategoryMV>();
           foreach(var mcategory in db.StockMenuCategoryTables.ToList())
            {
                var username= db.UserTables.Find(mcategory.CreatedBy_User).Username;
                Lists.Add(new StockMenuCategoryMV()
                {
                    StockMenuCategoryID = mcategory.StockMenuCategoryID,
                    StockMenuCategory = mcategory.StockMenuCategory,
                    CreatedBy = username
                });
            }
        }

    }
}