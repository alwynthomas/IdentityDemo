using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityDemo.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(string connectionString)
            : base(new UserStore<AppUser>(new AppIdentityDbContext(connectionString)))
        {
            //set default properties for user manager here
            this.DefaultAccountLockoutTimeSpan = new TimeSpan(0, 10, 0);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;
            this.UserLockoutEnabledByDefault = true;
        }
    }
}