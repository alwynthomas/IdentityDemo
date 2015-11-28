using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityDemo.Identity
{
    public class AppUserManagerContainer
    {
        public AppUserManager Get()
        {
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["dbCon"].ToString();
            return new AppUserManager(con);
        }
    }
}