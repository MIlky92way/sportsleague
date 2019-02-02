using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CinemaStore.Auth.IdentityConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaStore.Controllers
{
    public class BaseController : Controller
    {
        protected CinemaStoreUserManager UserManager
        {
            get
            {
                var userManager = HttpContext
                    .GetOwinContext()
                    .GetUserManager<CinemaStoreUserManager>();

                return userManager;
            }
        }
        protected IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext
                    .GetOwinContext()
                    .Authentication;
            }
        }
        protected BaseController()
        {

        }
    }
}