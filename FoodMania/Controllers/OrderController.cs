using Dblayer;
using FoodMania.Models;
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
        public ActionResult ViewCart()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);

            var cart = Db.CartTables.Where(u => u.UserID == userid).FirstOrDefault();
            var cartdetails = new List<CartMV>();
            int cartid = cart != null ? cart.CartID : 0;
            foreach (var cart_item in Db.CartItemDetailTables.Where(i => i.CartID == cartid).ToList())
            {
                var stockitem = Db.StockItemTables.Find(cart_item.StockItemID);
                cartdetails.Add(new CartMV()
                {
                    CartID = cart.CartID,
                    CartItemID = cart_item.CartItemID,
                    StockItemID = cart_item.StockItemID,
                    StockItemTitle = stockitem.StockItemTitle,
                    ItemPhotoPath = stockitem.ItemPhotoPath,
                    ItemSize = stockitem.ItemSize,
                    UnitPrice = stockitem.UnitPrice,
                    Qty = cart_item.Qty,
                    ItemCost = stockitem.UnitPrice * cart_item.Qty,
                    ItemType = "item"
                });
            }

            foreach (var cart_deal in Db.CartDealTables.Where(d => d.CartID == cartid).ToList())
            {
                var stockdeal = Db.StockDealTables.Find(cart_deal.StockDealID);
                cartdetails.Add(new CartMV()
                {
                    CartID = cart.CartID,
                    CartItemID = cart_deal.CartDealID,
                    StockItemID = cart_deal.StockDealID,
                    StockItemTitle = stockdeal.StockDealTitle,
                    ItemPhotoPath = stockdeal.DealPhoto,
                    ItemSize = "Normal",
                    UnitPrice = stockdeal.DealPrice,
                    Qty = cart_deal.Qty,
                    ItemCost = stockdeal.DealPrice * cart_deal.Qty,
                    ItemType = "Deal"
                });
            }


            return View(cartdetails);
        }
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
        public ActionResult Add_Qty(int? cartitemid, int? qty, string itemtype)
        {
            bool result = false;
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    if (itemtype == "item")
                    {
                        var item = Db.CartItemDetailTables.Where(i => i.CartItemID == cartitemid).FirstOrDefault();
                        if (item != null)
                        {
                            item.Qty = item.Qty + 1;
                            Db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            Db.SaveChanges();
                        }
                    }
                    else
                    {
                        var deal = Db.CartDealTables.Where(d => d.CartDealID == cartitemid).FirstOrDefault();
                        if (deal != null)
                        {
                            deal.Qty = deal.Qty + 1;
                            Db.Entry(deal).State = System.Data.Entity.EntityState.Modified;
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
        public ActionResult Minus_Qty(int? cartitemid, int? qty, string itemtype)
        {
            bool result = false;
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    if (itemtype == "item")
                    {
                        var item = Db.CartItemDetailTables.Where(i => i.CartItemID == cartitemid).FirstOrDefault();
                        if (item != null)
                        {
                            if (item.Qty == 1)
                            {
                                Db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                                Db.SaveChanges();
                            }
                            else
                            {
                                item.Qty = item.Qty - 1;
                                Db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                                Db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        var deal = Db.CartDealTables.Where(d => d.CartDealID == cartitemid).FirstOrDefault();
                        if (deal != null)
                        {
                            if (deal.Qty == 1)
                            {
                                Db.Entry(deal).State = System.Data.Entity.EntityState.Deleted;
                                Db.SaveChanges();
                            }
                            else
                            {
                                deal.Qty = deal.Qty - 1;
                                Db.Entry(deal).State = System.Data.Entity.EntityState.Modified;
                                Db.SaveChanges();
                            }
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
        public ActionResult Delete_Item(int? cartitemid, string itemtype)
        {
            bool result = false;
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    if (itemtype == "item")
                    {
                        var item = Db.CartItemDetailTables.Where(i => i.CartItemID == cartitemid).FirstOrDefault();
                        if (item != null)
                        {

                            Db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                            Db.SaveChanges();
                        }
                    }
                    else
                    {
                        var deal = Db.CartDealTables.Where(d => d.CartDealID == cartitemid).FirstOrDefault();
                        if (deal != null)
                        {
                            deal.Qty = deal.Qty + 1;
                            Db.Entry(deal).State = System.Data.Entity.EntityState.Modified;
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