using FoodMania.Models;
using Dblayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodMania.Controllers
{
    public class HomeController : Controller
    {
        FoodManiaEntities Db = new FoodManiaEntities();
        // GET: Home
        public ActionResult Index()
        {
            var home = new HomeMV();
            return View(home);
        }

        public ActionResult SubscribeEmail(string subscribe_email)
        {
            var check = Db.SubscribeEmailTables.Where(e => e.SubscribeEmailAddress.Trim().ToUpper() == subscribe_email.Trim().ToUpper()).FirstOrDefault();
            if (check == null)
            {
                var email = new SubscribeEmailTable() { SubscribeEmailAddress = subscribe_email, SubscribeStatus = true };
                Db.SubscribeEmailTables.Add(email);
                Db.SaveChanges();

            }

            else
            {
                check.SubscribeStatus = true;
                Db.Entry(check).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult AllItems()
        {
            var list = new StockMenuMV();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllItems(StockMenuMV stockMenuMV)
        {
            var list = new StockMenuMV(stockMenuMV.SearchKey, stockMenuMV.categorylist, stockMenuMV.ordertypelist);
            return View(list);
        }

    }
}