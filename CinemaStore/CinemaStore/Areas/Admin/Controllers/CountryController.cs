using CinemaStore.Areas.Admin.Models;
using CinemaStore.Controllers;
using CinemaStore.Entities.Country;
using CinemaStore.Blogic.Base;
using CinemaStore.Blogic.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaStore.Areas.Admin.Controllers
{
    //[Authorize]
    public class CountryController : BaseController
    {
        private IBlogic<CountryEntity> countrySrv;
        public CountryController(IBlogic<CountryEntity> countrySrv)
        {
            this.countrySrv = countrySrv;
        }

        // GET: Admin/Country
        public ActionResult Index(CountryModel model)
        {
            model = new CountryModel();
            model.Countries = countrySrv
                .GetEntries(model);

            return View(model);
        }


        public ViewResult Edit(int id = 0)
        {
            CountryEntity model;

            if (id > 0)
            {
                model = countrySrv[id];
            }
            else
            {
                model = new CountryEntity();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CountryEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model = countrySrv.Edit(model);
                    if (model != null)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Во время редактирования произошли ошибки!");
                }
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            CountryEntity model = new CountryEntity() { Id = id };
            if (ModelState.IsValid)
            {
                try
                {
                    countrySrv.Delete(model);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Во время удаления произошли ошибки!");
                }
            }

            return View(model);
        }


    }
}