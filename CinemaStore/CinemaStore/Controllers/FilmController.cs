using CinemaStore.Entities.Category;
using CinemaStore.Entities.Film;
using CinemaStore.Models.Film;
using CinemaStore.Blogic.Base;
using CinemaStore.Blogic.Film;
using CinemaStore.Blogic.Film.Model;
using CinemaStore.Blogic.FilmCrew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CinemaStore.Infrastructure;
using CinemaStore.Blogic.Poster;
using CinemaStore.Entities.PosterImage;

namespace CinemaStore.Controllers
{
    public class FilmController : BaseController
    {
        private IFilm filmSrv;
        private IBlogic<CategoryEntity> categorySrv;
        private IBlogic<FilmCrewEntity> filmCrewSrv;
        private IPoster posterSrv;
        public FilmController(IFilm filmSrv, IBlogic<CategoryEntity> categorySrv, IBlogic<FilmCrewEntity> filmCrewSrv, IPoster posterSrv)
        {
            this.filmSrv = filmSrv;
            this.filmCrewSrv = filmCrewSrv;
            this.categorySrv = categorySrv;
            this.posterSrv = posterSrv;
        }
        #region props
        private MultiSelectList MultiSelectCategories
        {
            get
            {
                return new MultiSelectList(categorySrv.Entries, "Id", "Name");
            }
        }

        private MultiSelectList MultiSelectProducer
        {
            get
            {
                int producerGroup =
                    (int)FilmCrewGroup.Producers;

                var items = filmCrewSrv
                    .Entries
                    .Where(x => x.Group == producerGroup)
                    .Select(x => new { x.Id, Name = $"{x.GivenName} {x.FamilyName}" });

                return new MultiSelectList(items, "Id", "Name");
            }
        }

        private MultiSelectList MultiSelectActors
        {
            get
            {
                int producerGroup =
                    (int)FilmCrewGroup.Actors;

                var items = filmCrewSrv
                    .Entries
                    .Where(x => x.Group == producerGroup)
                    .Select(x => new { x.Id, Name = $"{x.GivenName} {x.FamilyName}" });

                return new MultiSelectList(items, "Id", "Name");
            }
        }
        #endregion

        //by id - category
        public ViewResult Index(FilmViewModel model)
        {
            var category = categorySrv[model.id];
            if (category != null)
            {
                model.CategoryName = category.Name;

            }

            model.Films = filmSrv.GetByCategory(model, model.id);

            return View(model);
        }

        public ViewResult Details(int id = 0)
        {
            var film = filmSrv[id];

            return View(film);
        }


        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            InitEditViewBag();
            string currentUserId = User.Identity.GetUserId();
            FilmEditViewModel model = new FilmEditViewModel();

            if (id > 0)
            {
                var entry = filmSrv[id];
                if (entry != null && entry.UserId == currentUserId)
                {
                    model.Name = entry.Name;
                    model.Year = entry.Year;
                    model.DateCreate = entry.DateCreate;
                    model.Description = entry.Description;
                    model.CategoryIds = entry
                        .Categories.Select(x => x.Id).ToArray();
                    model.ProducerIds = entry.Producer
                        .Select(x => x.Id).ToArray();
                    model.ActorIds = entry.Actors
                        .Select(x => x.Id).ToArray();
                }
                else
                {
                    ViewBag.Allowed = false;
                    return Redirect("~/profile");
                }
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FilmEditViewModel model)
        {
            InitEditViewBag();

            if (ModelState.IsValid)
            {
                try
                {
                    model.UserId = User.Identity.GetUserId();
                    var result = filmSrv.Edit(model);
                    if (result != null && result.Id > 0)
                    {
                        SavePoster(model.Files, result.Id);
                    }
                    return Redirect("~/profile");
                }
                catch
                {
                    ModelState.AddModelError("", "При редактировании произошли ошибки!");
                }
            }

            return View(model);
        }
        private void InitEditViewBag()
        {
            ViewBag.SelectCategories = MultiSelectCategories;
            ViewBag.SelectProducer = MultiSelectProducer;
            ViewBag.SelectActors = MultiSelectActors;
        }

        private void SavePoster(HttpPostedFileBase[] files, int filmId)
        {
            if (files == null || filmId == 0)
            {
                return;
            }

            var image = files[0];

            if (image != null)
            {
                DirectoryTools.CheckDirectoryExist(ImageOptions.PATH);

                var posters = posterSrv[filmId];
                DeletePoster(posters);

                var poster = new PosterImageEntity
                {
                    FilmId = filmId,
                    MaxImage = ImageResize
                     .Resize(image, ImageOptions.PATH, ImageOptions.IMAGE_WIDTH_MAX_VERTICAL, ImageOptions.IMAGE_HEIGHT_MAX_VERTICAL),
                    MinImage = ImageResize
                     .Resize(image, ImageOptions.PATH, ImageOptions.IMAGE_WIDTH_MIN_VERTICAL, ImageOptions.IMAGE_HEIGHT_MIN_VERTICAL)
                };

                posterSrv.Add(poster);
            }
        }

        private void DeletePoster(List<PosterImageEntity> posters)
        {
            foreach (var item in posters ?? new List<PosterImageEntity>())
            {
                posterSrv.Delete(item);
                DirectoryTools.DeleteFile(HttpContext.Server.MapPath, item.MaxImage);
                DirectoryTools.DeleteFile(HttpContext.Server.MapPath, item.MinImage);
            }
        }
    }
}