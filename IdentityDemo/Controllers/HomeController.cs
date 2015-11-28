using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.User = ((ClaimsIdentity)User.Identity).Claims.Single(c => c.Type == ClaimTypes.Name).Value;
            return View();
        }
    }
}