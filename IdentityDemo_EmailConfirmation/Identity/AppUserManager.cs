using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityDemo.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(string con)
            : base(new UserStore<AppUser>(
                new AppIdentityDbContext(con)
            ))
        {
            //set default properties for user manager here
            this.DefaultAccountLockoutTimeSpan = new TimeSpan(0, 10, 0);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;
            this.UserLockoutEnabledByDefault = false;

            this.EmailService = new UserEmailService();

            //DataProtectorTokenProvider needs Install-Package Microsoft.AspNet.Identity.Owin
            var provider = new DpapiDataProtectionProvider("IdentityDemo");
            this.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(provider.Create(new string[] { "EmailConfirmation" }));
        }
    }
}
