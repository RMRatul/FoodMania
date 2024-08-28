using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodMania.Models
{
    public class OrderTypeMV
    {
        public int OrderTypeID { get; set; }
       
        public string OrderType { get; set; }
    }
}