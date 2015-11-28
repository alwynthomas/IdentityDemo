using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace IdentityDemo.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult index(Models.Register model)
        {
            if(ModelState.IsValid)
            {
                var UserManager = new Identity.AppUserManagerContainer().Get();

                //see if user already exists
                var existingUser = UserManager.FindByName(model.Username);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "A user with the given username already exists.";
                }
                else
                {
                    //create the user
                    var User = new Identity.AppUser()
                    {
                        UserName = model.Username,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.EmailAddress,
                        AccountEnabled = true
                    };

                    var result = UserManager.Create(User, model.Password);
                    if (result.Succeeded)
                    {
                        return Redirect("Login");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to create an account an error occured.";
                    }
                }
            }
            return View(model);
        }

    }
}