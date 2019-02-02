using CinemaStore.Blogic.Film;
using CinemaStore.Models.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaStore.Controllers
{
    public class HomeController : BaseController
    {
        private IFilm filmSrv;
        public HomeController(IFilm filmSrv)
        {
            this.filmSrv = filmSrv;
        }
        public ActionResult Index(FilmViewModel model)
        {
            model.Films = filmSrv.GetByCategory(model, model.id);

            return View(model);
        }
    }
}