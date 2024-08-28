using Dblayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodMania.Models
{
    public class CRU__OrderTypeMV
    {
        FoodManiaEntities db = new FoodManiaEntities();

        public CRU__OrderTypeMV()
        {
            GetData();
        }

        public CRU__OrderTypeMV(int? id)
        {
            GetData();
            var edit = db.OrderTypeTables.Find(id);
            if(edit != null) 
            {
                OrderTypeID = edit.OrderTypeID;
                OrderType= edit.OrderType;
            }
            else
            {
                OrderTypeID = 0;
                OrderType=string.Empty;
            }
        }
        public int OrderTypeID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string OrderType { get; set; }

        public virtual List<OrderTypeMV>Lists { get; set; }

        public void GetData()
        {
            Lists= new List<OrderTypeMV>();
            foreach(var ordertype in db.OrderTypeTables.ToList())
            {
                Lists.Add(new OrderTypeMV(){
                    OrderTypeID=ordertype.OrderTypeID,
                    OrderType=ordertype.OrderType
                });
            }
        }
    }
}