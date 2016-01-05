using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace IdentityDemo.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public ActionResult Index(string returnUrl)
        {
            var model = new Models.Login();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Models.Login model)
        {
            if(ModelState.IsValid)
            {

                var UserManager = new Identity.AppUserManagerContainer().Get();

                var User = UserManager.FindByName(model.Username);
                if (User != null)
                {

                    if (!User.EmailConfirmed)
                        ViewBag.ErrorMessage = "You must verify your email account before logging in.";

                    else
                    {

                        if (!User.AccountEnabled)
                            ViewBag.ErrorMessage = "This account has been disabled.";

                        else
                        {
                            if (UserManager.IsLockedOut(User.Id))
                                ViewBag.ErrorMessage = "Account has been temporarily locked out.";

                            else
                            {

                                var validUser = UserManager.Find(model.Username, model.Password);
                                if (validUser != null)
                                {

                                    //log the user in
                                    var ident = new ClaimsIdentity(new[] {
                                    new Claim(ClaimTypes.Name, string.Concat(User.FirstName, " ", User.LastName)),
                                    new Claim(ClaimTypes.Email, User.Email)
                                }, "ApplicationCookie");
                                    var ctx = Request.GetOwinContext();
                                    var authManager = ctx.Authentication;
                                    authManager.SignIn(ident);
                                    authManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, ident);

                                    //redirect to login
                                    return RedirectToAction("", "Home");

                                }
                                else
                                {
                                    //login failed
                                    UserManager.AccessFailed(User.Id);
                                    if (UserManager.IsLockedOut(User.Id))
                                        ViewBag.ErrorMessage =
                                            "Your account has now been temporarily locked out due to too many failed login attempts.";
                                    else
                                        ViewBag.ErrorMessage =
                                            "Login failed.";

                                }

                            }//end user lock out test

                        }//end account enabled test

                    }//end email confirmed test

                }
                else//could not find user
                    ViewBag.ErrorMessage = "Login failed.";

            }
            return View(model);
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

    }
}