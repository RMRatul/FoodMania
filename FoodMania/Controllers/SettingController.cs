using System;
using System.Collections.Generic;
using FoodMania.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dblayer;

namespace FoodMania.Controllers
{
   
    public class SettingController : Controller
    {
        FoodManiaEntities Db = new FoodManiaEntities();
        // GET: Setting
        public ActionResult List_UserTypes(int? id)
        {
            var usertype = new CRU_UserTypeMV(id);
            return View(usertype);
        }

        [HttpPost]
        public ActionResult List_UserTypes(CRU_UserTypeMV cRU_UserTypeMV)
        {
            if (ModelState.IsValid)
            {
                if (cRU_UserTypeMV.UserTypeID == 0)
                {
                    var checkexist = Db.UserTypeTables.Where(u => u.UserType == cRU_UserTypeMV.UserType).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var usertype = new UserTypeTable();
                        usertype.UserType = cRU_UserTypeMV.UserType;
                        Db.UserTypeTables.Add(usertype);
                        Db.SaveChanges();
                        return RedirectToAction(" List_UserTypes", new { id = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("UserType", "All Ready Exist!");
                    }
                }
                else
                {
                    var checkexist = Db.UserTypeTables.Where(u => u.UserType == cRU_UserTypeMV.UserType && u.UserTypeID != cRU_UserTypeMV.UserTypeID).FirstOrDefault();
                    if (checkexist == null)
                    {
                        var usertype = Db.UserTypeTables.Find(cRU_UserTypeMV.UserTypeID);
                        usertype.UserType = cRU_UserTypeMV.UserType;
                        Db.Entry(usertype).State = System.Data.Entity.EntityState.Modified;
                        Db.SaveChanges();
                        return RedirectToAction(" List_UserTypes", new { id = 0 });
                    }
                    else
                    {
                        ModelState.AddModelError("UserType", "All Ready Exist!");
                    }
                }
            }
            return View(cRU_UserTypeMV);
        }

    }
}