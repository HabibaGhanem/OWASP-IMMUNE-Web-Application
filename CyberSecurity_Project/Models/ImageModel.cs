using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
namespace CyberSecurity_Project.Models
{
    public class ImageModel
    {
        public string ErrorMessage { get; set; }
        public decimal imagesize { get; set; }
        //HttpPostedFileBase: base class for classes that provide access to individual image uploaded by client
        public string UploadUserImage(HttpPostedFileBase image)
        {
            try
            {
                var imageTypes = new[] {"png", "jpg" };
           //GetExtension method is used to get the extension of uploaded image From the path of the system
                var imageExt = System.IO.Path.GetExtension(image.FileName).Substring(1);
                //image is uploaded by the user, it can be checked as valid or not
                if (!imageTypes.Contains(imageExt))
                {
                    ErrorMessage = "Image Extension Is InValid - Only Upload png/jpg";
                    return ErrorMessage;
                }
                //check for image size validation
                else if (image.ContentLength > (imagesize * 1024))
                {
                    ErrorMessage = "Image size Should Be UpTo " + imagesize + "KB";
                    return ErrorMessage;
                }
                else
                {
                    ErrorMessage = "Image Is Successfully Uploaded";
                    return ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Upload Container Should Not Be Empty or Contact Admin";
                return ErrorMessage;
            }
        }
    }
}