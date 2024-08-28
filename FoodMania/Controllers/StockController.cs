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

    }
}