using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Identity
{
    public class ApplicationUser:IdentityUser
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }


    }
}