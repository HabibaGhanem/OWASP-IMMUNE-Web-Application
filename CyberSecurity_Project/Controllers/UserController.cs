using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using CyberSecurity_Project.Models;
using System.Data.SqlClient;
using Google.Authenticator;
using System.Web.Routing;
using System.Diagnostics;

namespace CyberSecurity_Project.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _context;
        public UserController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Register()
        {   
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login", "User");
        }
        public ActionResult Index() {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            //get the user with the entered username from the database            
            var u = _context.Users.SingleOrDefault(m => m.username == user.username);
            
            if (u != null) //if the user is found
            {
                if (user.password == u.password) //comparing the entered password with the password                                                 //stored in the database
                {
                    Session["username"] = u.username; //set the current user
                    if (u.TwoFactorEnabled)
                    {
                        return Redirect("/User/Authorize");
                    }
                    return RedirectToAction("Enable", "User");
                }
                else
                {                    
                    ViewBag.ErrorMessage = "Invalid username or password";                   
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password";               
            }
            
            return View(u);
        }
        
        public ActionResult welcome()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(String search)
        {
            /* Vulnerable to SQL injection
            String sql = "Select * from Users where username ='" + search + "'";
            var user = _context.Users.SqlQuery(sql).ToList();
           */
            /*Entity framework uses LINQ-to-Entities parameterized queries, 
            and it is not susceptible to traditional SQL injection attacks
            var sql = "Select * from Users where username = @u";
            SqlParameter parameter = new SqlParameter("@u", search);
            var user = _context.Users.SqlQuery(sql, parameter).ToList();
            */
            
            bool isInjected = Sql_InjectionDetector.checkForSQLInjection(search);
            if (isInjected)
            {
                return RedirectToAction("SearchResult", "User");
            }
            else
            {
                var user = _context.Users.Where(m => m.username == search).ToList();
                return View(user);
            }
        }
        public ActionResult Search()
        {
            return View();
        }
       
        
        public ActionResult SearchResult()
        {
            return View();
        }
       
        [HttpGet]
        public ActionResult Enable()
        {
            // TODO: fetch signed in user from a database
            String u = Session["username"].ToString();
            var user = _context.Users.SingleOrDefault(m => m.username == u); 
            TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
            var setupInfo =
                twoFactor.GenerateSetupCode("CyberSecurity_Project", user.username, TwoFactorKey(user), false, 3);
            ViewBag.SetupCode = setupInfo.ManualEntryKey;
            ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Enable(string inputCode)
        {
            // TODO: fetch signed in user from a database

            String u = Session["username"].ToString();
            var user = _context.Users.SingleOrDefault(m => m.username == u);
            TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
            bool isValid = twoFactor.ValidateTwoFactorPIN(TwoFactorKey(user), inputCode);
            if (!isValid)
            {
                return Redirect("/User/Enable");
            }

            user.TwoFactorEnabled = true;
            // TODO: store the updated user in database
            var userInDB = _context.Users.SingleOrDefault(m => m.username == user.username);
            userInDB.TwoFactorEnabled = user.TwoFactorEnabled;
            _context.SaveChanges();
            if (user.username == "admin")
            {
                return Redirect("/User/Adminpage");
            }
            //else
            return Redirect("/User/welcome");
        }
        private static string TwoFactorKey(User user)
        {
            return $"myverysecretkey+{user.username}";
        }
        
        [HttpGet]
        public ActionResult Authorize()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(string inputCode)
        {
            // TODO: fetch signed in user from a database
            String u = Session["username"].ToString();
            var user = _context.Users.SingleOrDefault(m => m.username == u);
            TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
            bool isValid = twoFactor.ValidateTwoFactorPIN(TwoFactorKey(user), inputCode);
            if (!isValid)
            {
                return Redirect("/User/Authorize");
            }
            if (user.username == "admin") {
                return Redirect("/User/Adminpage");
            }
            //else
            // TODO: Sign in the user
            return Redirect("/User/welcome");
        }

        //Admin part

        public ActionResult fail()
        {
            return View();
        }

        [NoDirectAccess]
        public ActionResult Adminpage()
        {
            return View();
        }











    }
}
/*
        [HttpPost]    
        public ActionResult login(User user)
        {
            //get the user with the entered username from the database
            var u = _context.Users.SingleOrDefault(m => m.username == user.username);

            if (u != null) //if the user is found
            {
                if (user.password == u.password) //comparing the entered password with the password
                                                 //stored in the database
                {
                    Session["username"] = u.username; //set the current user
                    
                    if (u.TwoFactorEnabled)
                    {
                        return Redirect("/User/Authorize");
                    }

                    return RedirectToAction("Enable","User");
                }
                else
                {
                    TempData["Invalid"] = 1;
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                TempData["Invalid"] = 1;

                return RedirectToAction("Index", "User");
            }
            
        }
            */

//default [HttpGet]
//var pass = AESEncrytDecry.DecryptStringAES(user.password);
//var user = (from u in _context.Users where u.username == search select u).ToList();