using Dblayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodMania.Models;

namespace FoodMania.Controllers
{
    public class StockController : Controller
    {

        FoodManiaEntities Db = new FoodManiaEntities();
        // GET: Stock
        public ActionResult StockItemCategory(int ?id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            var stockcategories =new CRU__StockItemCategoryMV(id);
            ViewBag.VisibleStatusID = new SelectList(Db.VisibleStatusTables.ToList(), "VisibleStatusID", "VisibleStatus", stockcategories.VisibleStatusID);
            return View(stockcategories);
        }

        [HttpPost]
        public ActionResult StockItemCategory(CRU__StockItemCategoryMV stockcategory)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }

            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]),out userid);
            stockcategory.CreatedBy_UserID = userid;
            if(ModelState.IsValid)
            {
                if(stockcategory.StockItemCategoryID==0)
                {
                    var checkexist = Db.StockItemCategoryTables.Where(s => s.StockItemCategory.Trim().ToUpper() == stockcategory.StockItemCategory.Trim().ToUpper()).FirstOrDefault();
                    if(checkexist==null) 
                    {
                        var newcategory = new StockItemCategoryTable();
                        newcategory.StockItemCategory = stockcategory.StockItemCategory;
                        newcategory.VisibleStatusID = stockcategory.VisibleStatusID;
                        newcategory.CreatedBy_UserID=userid;
                        Db.StockItemCategoryTables.Add(newcategory);
                        Db.SaveChanges();
                        return RedirectToAction("StockItemCategory", new {id=0});
                    }

                    else
                    {
                        ModelState.AddModelError("StockItemCategory", "Already Registered");
                    }
                }
                else 
                {
                    var checkexist = Db.StockItemCategoryTables.Where(s => s.StockItemCategory.Trim().ToUpper() == stockcategory.StockItemCategory.Trim().ToUpper() && s.StockItemCategoryID != stockcategory.StockItemCategoryID).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var editcategory = Db.StockItemCategoryTables.Find(stockcategory.StockItemCategoryID);
                        editcategory.StockItemCategory = stockcategory.StockItemCategory;
                        editcategory.VisibleStatusID = stockcategory.VisibleStatusID;
                        editcategory.CreatedBy_UserID = userid;
                        Db.Entry(editcategory).State=System.Data.Entity.EntityState.Modified;
                        Db.SaveChanges();
                        return RedirectToAction("StockItemCategory", new { id = 0 });
                    }

                    else
                    {
                        ModelState.AddModelError("StockItemCategory", "Already Registered");
                    }

                }
            }


            ViewBag.VisibleStatusID = new SelectList(Db.VisibleStatusTables.ToList(), "VisibleStatusID", "VisibleStatus", stockcategory.VisibleStatusID);
            return View(stockcategory); 
        }

        public ActionResult OrderType(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            var ordertype =new CRU__OrderTypeMV(id);
            return View(ordertype);

        }

        [HttpPost]
        public ActionResult OrderType(CRU__OrderTypeMV orderTypeMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }

            if(ModelState.IsValid)
            {
                if (orderTypeMV.OrderTypeID == 0)
                {
                    var checkexist = Db.OrderTypeTables.Where(o => o.OrderType.Trim().ToUpper() == orderTypeMV.OrderType.Trim().ToUpper()).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var newordertype = new OrderTypeTable();
                        newordertype.OrderType = orderTypeMV.OrderType;
                        Db.OrderTypeTables.Add(newordertype);
                        Db.SaveChanges();
                        return RedirectToAction("OrderType", new { id = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("OrderType", "Already Exist!");
                    }
                }
                else
                {
                    var checkexist = Db.OrderTypeTables.Where(o => o.OrderType.Trim().ToUpper() == orderTypeMV.OrderType.Trim().ToUpper() && o.OrderTypeID !=orderTypeMV.OrderTypeID).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var editordertype = Db.OrderTypeTables.Find(orderTypeMV.OrderTypeID);
                        editordertype.OrderType = orderTypeMV.OrderType;
                        Db.Entry(editordertype).State=System.Data.Entity.EntityState.Modified;
                        Db.SaveChanges();
                        return RedirectToAction("OrderType", new { id = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("OrderType", "Already Exist!");
                    }
                }
            }
            return View(orderTypeMV);
        }


        public ActionResult StockMenuCategory(int? id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            var menucategories = new CRU_StockMenuCategoryMV(id);
           
            return View(menucategories);
        }


        [HttpPost]
        public ActionResult StockMenuCategory(CRU_StockMenuCategoryMV menucategory)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Index", "Home");
            }

            int userid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            menucategory.CreatedBy_User = userid;
            if (ModelState.IsValid)
            {
                if (menucategory.StockMenuCategoryID == 0)
                {
                    var checkexist = Db.StockMenuCategoryTables.Where(s => s.StockMenuCategory.Trim().ToUpper() == menucategory.StockMenuCategory.Trim().ToUpper()).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var newcategory = new StockMenuCategoryTable();
                        newcategory.StockMenuCategory = menucategory.StockMenuCategory;
                        
                        newcategory.CreatedBy_User = userid;
                        Db.StockMenuCategoryTables.Add(newcategory);
                        Db.SaveChanges();
                        return RedirectToAction("StockMenuCategory", new { id = 0 });
                    }

                    else
                    {
                        ModelState.AddModelError("StockMenuCategory", "Already Registered");
                    }
                }
                else
                {
                    var checkexist = Db.StockMenuCategoryTables.Where(s => s.StockMenuCategory.Trim().ToUpper() == menucategory.StockMenuCategory.Trim().ToUpper() && s.StockMenuCategoryID != menucategory.StockMenuCategoryID).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var editcategory = Db.StockMenuCategoryTables.Find(menucategory.StockMenuCategoryID);
                        editcategory.StockMenuCategory = menucategory.StockMenuCategory;
                        
                        editcategory.CreatedBy_User = userid;
                        Db.Entry(editcategory).State = System.Data.Entity.EntityState.Modified;
                        Db.SaveChanges();
                        return RedirectToAction("StockMenuCategory", new { id = 0 });
                    }

                    else
                    {
                        ModelState.AddModelError("StockMenuCategory", "Already Registered");
                    }

                }
            }


          
            return View(menucategory);
        }



    }
}