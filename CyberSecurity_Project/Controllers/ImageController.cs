
using CyberSecurity_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CyberSecurity_Project.Controllers
{
    public class ImageController : Controller
    {
        //    
        // GET: /File/    

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult imageupload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult imageupload(HttpPostedFileBase image)
        {
            ImageModel myimage = new ImageModel();
            myimage.imagesize = 550;
            string usermessage = myimage.UploadUserImage(image);
            //If the uploaded value is not empty or null, then all warnings and successful messages will come 
            if (usermessage != null)
            {
                ViewBag.ResultErrorMessage = myimage.ErrorMessage;
            }
            return View();
        }
    }
}