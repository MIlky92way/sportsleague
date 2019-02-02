using CinemaStore.Models.Account;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CinemaStore.Auth.IdentityConfig;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaStore.Entities.Auth;

namespace CinemaStore.Controllers
{
    public class AccountController : BaseController
    {
       
        private CinemaStoreSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<CinemaStoreSignInManager>();
            }
        }
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Find(model.Email, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Пользователь или пароль введены неверно.");
                }
                else
                {
                    SignInManager.SignIn(user, false, model.RememberMe);

                    return Redirect("~/Profile");
                }
            }
            return View(model);
        }

        public ViewResult Register(string returnUrl)
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            IdentityResult identResult;
            CinemaStoreUser user = new CinemaStoreUser();
            user.Email = model.Email;
            user.UserName = model.Email;
            if (ModelState.IsValid)
            {
                identResult = UserManager.Create(user, model.Password);

                if (identResult.Succeeded)
                {
                    SignInManager.SignIn(user, false, model.RememberMe);

                    return Redirect("~/Profile");
                }
                else
                {
                    InitIdentityResultErorr(identResult);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AuthManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return Redirect("~/Home/Index");
        }

        #region common methods
        private void InitIdentityResultErorr(IdentityResult identResult)
        {
            foreach (var item in identResult.Errors)
            {
                ModelState.AddModelError("", item);
            }
        }
        #endregion
    }
}