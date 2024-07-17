using Dblayer;
using FoodMania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodMania.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        FoodManiaEntities Db = new FoodManiaEntities();
        public ActionResult Login()
        {
            var user = new LoginMV();
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(LoginMV loginMV)
        {

            if (ModelState.IsValid)
            {
                var user = Db.UserTables.Where(u =>(u.EmailAddress == loginMV.UserName.Trim() || u.Username.Trim() == loginMV.UserName.Trim()) && u.Password.Trim() == loginMV.Password.Trim()).FirstOrDefault();
                if (user != null)
                {
                    if (user.UserStatusID == 1)
                    {
                        Session["UserID"] = user.UserID;
                        Session["UserTypeID"] = user.UserTypeID;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        var accountstatus = Db.UserStatusTables.Find(user.UserStatusID);
                        ModelState.AddModelError(string.Empty, "Account is " + accountstatus.UserStatus + "");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Enter Correct User Name and Password!");
                }
            }
            Session["UserID"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            return View(loginMV);
        }
        public ActionResult Logout()
        {
            Session["UserID"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            var user = new Reg_UserMV();
            ViewBag.GenderID = new SelectList(Db.GenderTables.ToList(), "GenderID", "GenderTittle", "0");
            return View(user);
        }

        [HttpPost]
        public ActionResult Register(Reg_UserMV reg_UserMV)
        {
            reg_UserMV.UserTypeID = 4; // Customer User_Type_ID
            reg_UserMV.RegisterationDate = DateTime.Now;
            reg_UserMV.UserStatusID = 1;
            if (ModelState.IsValid)
            {
                bool isexist = false;
                var checkexist = Db.UserTables.Where(u => u.Username.ToUpper().Trim() == reg_UserMV.UserName.ToUpper().Trim()).FirstOrDefault();
                if (checkexist != null)
                {
                    isexist = true;
                    ModelState.AddModelError("UserName", "Already Exist!");
                }
                checkexist = Db.UserTables.Where(u => u.EmailAddress.ToUpper().Trim() == reg_UserMV.EmailAddress.ToUpper().Trim()).FirstOrDefault();
                if (checkexist != null)
                {
                    isexist = true;
                    ModelState.AddModelError("EmailAddress", "Already Exist!");
                }
                if (isexist == false)
                {
                    var user = new UserTable();
                    user.UserTypeID = reg_UserMV.UserTypeID;
                    user.Username = reg_UserMV.UserName;
                    user.Password = reg_UserMV.Password;
                    user.FirstName = reg_UserMV.FirstName;
                    user.LastName = reg_UserMV.LastName;
                    user.ContactNo = reg_UserMV.ContactNo;
                    user.GenderID = reg_UserMV.GenderID;
                    user.EmailAddress = reg_UserMV.EmailAddress;
                    user.RegisterationDate = reg_UserMV.RegisterationDate;
                    user.UserStatusID = reg_UserMV.UserStatusID;
                    Db.UserTables.Add(user);
                    Db.SaveChanges();
                    return RedirectToAction("Login", "User");
                }
            }
            ViewBag.GenderID = new SelectList(Db.GenderTables.ToList(), "GenderID", "GenderTittle", reg_UserMV.GenderID);
            return View(reg_UserMV);
        }



    }
}