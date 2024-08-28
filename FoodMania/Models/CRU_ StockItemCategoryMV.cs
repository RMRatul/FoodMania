using Dblayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodMania.Models
{
    public class CRU__StockItemCategoryMV
    {
        FoodManiaEntities db = new FoodManiaEntities();
        public CRU__StockItemCategoryMV()
        {
            GetAllCategories();
        }

        public CRU__StockItemCategoryMV(int?id)
        {
            GetAllCategories();
            var edit=db.StockItemCategoryTables.Find(id);
            if(edit != null)
            {
                StockItemCategoryID=edit.StockItemCategoryID;
                StockItemCategory=edit.StockItemCategory;
                VisibleStatusID=edit.VisibleStatusID;

            }
            else
            {
                StockItemCategoryID=0;
                StockItemCategory=string.Empty;
                VisibleStatusID = 0;
            }
        }
        public int StockItemCategoryID { get; set; }
        public string StockItemCategory { get; set; }
        public int CreatedBy_UserID { get; set; }
        public int VisibleStatusID { get; set; }

        public virtual List<StockItemCategoryMV>Lists { get; set; }

        private void GetAllCategories()
        {
            Lists = new List<StockItemCategoryMV>();
            foreach (var category in db.StockItemCategoryTables.ToList())
            {
                var username = db.UserTables.Find(category.CreatedBy_UserID).Username;
                var status = db.VisibleStatusTables.Find(category.VisibleStatusID).VisibleStatus;
                Lists.Add(new StockItemCategoryMV()
                {

                      StockItemCategoryID= category.StockItemCategoryID,
         StockItemCategory = category.StockItemCategory,
                    CreatedBy = username,
                    VisibleStatus =status,

                });
            }
        }

    }
}