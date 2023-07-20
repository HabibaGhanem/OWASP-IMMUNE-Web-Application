using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CyberSecurity_Project.Models
{
    public class User
    {
        public int id { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public bool TwoFactorEnabled { get; set; }

    }
}