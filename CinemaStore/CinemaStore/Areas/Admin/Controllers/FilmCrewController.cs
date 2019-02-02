using CinemaStore.Areas.Admin.Models;
using CinemaStore.Entities.Film;
using CinemaStore.Blogic.FilmCrew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaStore.Areas.Admin.Controllers
{
    public class FilmCrewController : Controller
    {
        private IFilmCrew filmcrewSrv;
        public FilmCrewController(IFilmCrew filmcrewSrv)
        {
            this.filmcrewSrv = filmcrewSrv;
        }
        public ActionResult Index(FilmCrewModel model)
        {
            model.FilmCrew = filmcrewSrv.GetEntries(model);
            return View(model);
        }

        public ViewResult Edit(int id = 0)
        {
            FilmCrewEditModel model = new FilmCrewEditModel();

            if (id > 0)
            {
                var entity = filmcrewSrv[id];

                model.GivenName = entity.GivenName;
                model.FamilyName = entity.FamilyName;
                model.Patronymic = entity.Patronymic;
                model.Group = entity.Group;
                model.Id = entity.Id;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(FilmCrewEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    filmcrewSrv.Edit(model);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Во время редаткирования произошли ошибки");
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            FilmCrewEntity model = new FilmCrewEntity { Id = id };

            if (id > 0)
            {
                filmcrewSrv.Delete(model);
            }

            return RedirectToAction("Index");
        }
    }
}