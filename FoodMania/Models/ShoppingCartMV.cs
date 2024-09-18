using Dblayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodMania.Models
{
    public class ShoppingCartMV
    {
        FoodManiaEntities Db = new FoodManiaEntities();
        public ShoppingCartMV(int userid)
        {
            Cart_Items = new List<CartMV>();
            var user = Db.UserTables.Find(userid);
            User_info = new User_ProfileMV();
            User_info.FirstName = user.FirstName;
            User_info.LastName = user.LastName;
            User_info.ContactNo = user.ContactNo;
            User_info.EmailAddress = user.EmailAddress;
        }
        public List<CartMV> Cart_Items { get; set; }
        public User_ProfileMV User_info { get; set; }
        public int UserAddressID { get; set; }
        public int OrderTypeID { get; set; }
    }
}