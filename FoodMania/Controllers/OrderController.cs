using Dblayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodMania.Controllers
{
    public class OrderController : Controller
    {
        FoodManiaEntities Db = new FoodManiaEntities();
        // GET: Order
        public ActionResult Cart_AddItem(int? itemid, int? qty, string itemtype, string return_url)
        {
            bool result = false;
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                result = false;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);

            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    var cart = Db.CartTables.Where(u => u.UserID == userid).FirstOrDefault();
                    if (cart == null)
                    {
                        var create_cart = new CartTable()
                        {
                            UserID = userid
                        };
                        Db.CartTables.Add(create_cart);
                        Db.SaveChanges();
                        cart = Db.CartTables.Where(u => u.UserID == userid).FirstOrDefault();
                    }

                    if (itemtype == "item")
                    {
                        var item = Db.CartItemDetailTables.Where(i => i.StockItemID == itemid && i.CartID == cart.CartID).FirstOrDefault();
                        if (item != null)
                        {
                            item.Qty = item.Qty + 1;
                            Db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            Db.SaveChanges();
                        }
                        else
                        {
                            var create_item = new CartItemDetailTable()
                            {
                                CartID = cart.CartID,
                                StockItemID = Convert.ToInt32(itemid),
                                Qty = Convert.ToInt32(qty)
                            };
                            Db.CartItemDetailTables.Add(create_item);
                            Db.SaveChanges();
                        }
                    }
                    else
                    {
                        var deal = Db.CartDealTables.Where(d => d.StockDealID == itemid && d.CartID == cart.CartID).FirstOrDefault();
                        if (deal != null)
                        {
                            deal.Qty = deal.Qty + 1;
                            Db.Entry(deal).State = System.Data.Entity.EntityState.Modified;
                            Db.SaveChanges();
                        }
                        else
                        {
                            var create_deal = new CartDealTable()
                            {
                                CartID = cart.CartID,
                                Qty = Convert.ToInt32(qty),
                                StockDealID = Convert.ToInt32(itemid)
                            };
                            Db.CartDealTables.Add(create_deal);
                            Db.SaveChanges();
                        }
                    }
                    result = true;
                    transaction.Commit();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    result = false;
                    transaction.Rollback();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}