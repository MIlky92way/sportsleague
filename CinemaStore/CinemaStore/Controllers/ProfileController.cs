using CinemaStore.Blogic.Film;
using CinemaStore.Entities.Auth;
using CinemaStore.Models.Common;
using CinemaStore.Models.Profile;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaStore.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private IFilm filmSrv;
        public ProfileController(IFilm filmSrv)
        {
            this.filmSrv = filmSrv;
        }
        
        public ActionResult Index(ProfileModel model)
        {
            string userId = User.Identity.GetUserId();
            CinemaStoreUser user = UserManager.FindById(userId);

            if (user == null)
            {
                AuthManager.SignOut();
            }
            else
            {
                model = new ProfileModel { Email = user.Email };
            }

            model.Films = filmSrv.GetFilms(model, x => x.UserId == userId);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileModel model)
        {
            CinemaStoreUser user = UserManager.FindById(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                user.UserName = model.GivenName;
            }

            return View(model);
        }

    }
}