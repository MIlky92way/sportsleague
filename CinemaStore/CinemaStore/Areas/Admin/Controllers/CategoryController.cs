using CinemaStore.Areas.Admin.Models;
using CinemaStore.Entities.Category;
using CinemaStore.Blogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaStore.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private IBlogic<CategoryEntity> categorySrv;
        public CategoryController(IBlogic<CategoryEntity> categrySrv)
        {
            this.categorySrv = categrySrv;
        }
        // GET: Admin/Category
        public ActionResult Index(CategoryModel model)
        {
            model.Categories = categorySrv.GetEntries(model);
            return View(model);
        }

        public ViewResult Edit(int id = 0)
        {
            CategoryEntity model = new CategoryEntity();

            if (id > 0)
            {
                model = categorySrv[id];
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryEntity model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model = categorySrv.Edit(model);
                    return RedirectToAction("Index");
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
            CategoryEntity model = new CategoryEntity { Id = id };
            try
            {
                categorySrv.Delete(model);
            }
            catch
            {
                ModelState.AddModelError("", "Во время удаления произошли ошибки!");
            }
            return RedirectToAction("Index");
        }
    }
}