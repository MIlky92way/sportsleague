using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using CinemaStore.Db.Context;
using CinemaStore.Entities.Film;
using CinemaStore.Blogic.Film.Model;
using CinemaStore.Blogic.Model;
using System.Data.Entity;
using CinemaStore.Entities.Category;
using System.Linq.Expressions;
using CinemaStore.Blogic.FilmCrew;

namespace CinemaStore.Blogic.Film
{
    public class Film : IFilm
    {
        private CinemaStoreContext context;

        public FilmModel this[int id] => MapTo(Get(id));

        public void Delete(FilmEntity entity)
        {
        }

        public FilmEntity Edit(FilmEditModel model)
        {
            FilmEntity entry = null;
            using (context = new CinemaStoreContext())
            {
                if (model.Id > 0)
                {
                    entry =
                        context.FilmEntity
                        .Include(x => x.FilmCrew)
                        .Include(x => x.Categories)
                        .FirstOrDefault(x => x.Id == model.Id);
                }
                else
                {
                    entry = new FilmEntity();
                    context.FilmEntity.Add(entry);
                }

                entry.Description = model.Description;
                entry.Name = model.Name;
                entry.Year = model.Year;
                entry.UserId = model.UserId;

                List<int> selectedFilmCrewIds = AgregateSelectedFilmCrewIds(model);
                var selectedFilmCrew = GetFilmCrew(selectedFilmCrewIds.ToArray(), context);
                var selectedCategories = GetCategories(model.CategoryIds, context);

                try
                {

                    UpdateFilmCrew(selectedFilmCrew, entry, context);
                    UpdateFilmCategories(selectedCategories, entry, context);

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return entry;
        }

        public List<FilmModel> GetFilms(PaginationModel model, Func<FilmEntity, bool> predicate = null)
        {
            List<FilmModel> result = new List<FilmModel>();

            using (context = new CinemaStoreContext())
            {
                model.TotalItems = context.FilmEntity.Count();
                var films = context.FilmEntity
                    .Include(x => x.Posters)
                    .Include(x => x.Categories)
                    .Include(x => x.FilmCrew)
                    .AsNoTracking();
                
                if (predicate != null)
                {
                    films = films.Where(predicate).AsQueryable();
                }

                var tempfilms = films.OrderBy(x => x.DateCreate)
                      .Skip(() => (model.Page - 1) * model.CountOnPage).Take(() => model.CountOnPage)
                      .ToList();

                result = MapTo(tempfilms);
            }

            return result;
        }

        public List<FilmModel> GetByCategory(PaginationModel model, int categoryId)
        {
            List<FilmModel> result = new List<FilmModel>();
            using (context = new CinemaStoreContext())
            {
                //model.TotalItems = context.FilmEntity.Count();

                var films = context.FilmEntity
                    .Include(x => x.Categories);

                if (categoryId > 0)
                {
                    films = films
                        .Where(f => f.Categories.Any(c => c.Id == categoryId));

                    model.TotalItems = films.Count();
                }
                else
                {
                    return result;
                }

                var tempfilms = films
                    .OrderBy(x => x.DateCreate)
                    .Skip((model.Page - 1) * model.CountOnPage)
                    .Take(model.CountOnPage)
                    .Include(x => x.Posters)
                    .Include(x => x.FilmCrew)
                    .ToList();

                result = MapTo(tempfilms);
            }
            return result;
        }

        #region common getters

        private FilmEntity Get(int id)
        {
            FilmEntity entity = null;

            using (context = new CinemaStoreContext())
            {
                entity = context
                    .FilmEntity
                    .Include(x => x.FilmCrew)
                    .Include(x => x.Categories)
                    .Include(x => x.Posters)
                    .Include(x => x.User)
                    .FirstOrDefault(x => x.Id == id);
            }

            return entity;
        }

        private IEnumerable<FilmCrewEntity> GetFilmCrew(int[] ids, CinemaStoreContext context = null)
        {
            context = context ?? new CinemaStoreContext();
            IEnumerable<FilmCrewEntity> result = context.FilmCrew.Where(x => ids.Any(y => y == x.Id));
            return result;
        }
        private IEnumerable<CategoryEntity> GetCategories(int[] ids, CinemaStoreContext context)
        {
            context = context ?? new CinemaStoreContext();
            IEnumerable<CategoryEntity> result = context.Category.Where(x => ids.Any(y => y == x.Id));
            return result;
        }

        #endregion

        #region common updaters
        private void UpdateFilmCrew(IEnumerable<FilmCrewEntity> selectedCrew, FilmEntity entity, CinemaStoreContext context)
        {
            var exist = entity.FilmCrew.ToList();

            foreach (var item in exist ?? new List<FilmCrewEntity>())
            {
                entity.FilmCrew.Remove(item);
            }

            foreach (var item in selectedCrew)
            {
                entity.FilmCrew.Add(item);
            }
        }

        private void UpdateFilmCategories(IEnumerable<CategoryEntity> selectedCategory, FilmEntity entity, CinemaStoreContext context)
        {
            var exist = entity.Categories.ToList();

            foreach (var item in exist ?? new List<CategoryEntity>())
            {
                entity.Categories.Remove(item);
            }

            foreach (var item in selectedCategory)
            {
                entity.Categories.Add(item);
            }
        }



        public List<FilmEntity> Get(params Expression<Func<FilmEntity, object>>[] includeExpressions)
        {
            List<FilmEntity> res = null;
            using (context = new CinemaStoreContext())
            {
                IQueryable<FilmEntity> set = context
                    .FilmEntity
                    .AsNoTracking();

                foreach (var includeItem in includeExpressions)
                {
                    set = set.Include(includeItem);
                }

            }
            return res;
        }

        #endregion

        #region map methods

        private List<FilmModel> MapTo(IEnumerable<FilmEntity> films)
        {
            List<FilmModel> result = new List<FilmModel>();
            FilmModel film = null;

            foreach (var item in films)
            {
                film = MapTo(item);
                result.Add(film);
            }

            return result;
        }

        private FilmModel MapTo(FilmEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var film = new FilmModel();
            film.Categories = entity.Categories.ToList();
            film.Producer = entity.FilmCrew.Where(x => x.Group == (int)FilmCrewGroup.Producers).ToList();
            film.Actors = entity.FilmCrew.Where(x => x.Group == (int)FilmCrewGroup.Actors).ToList();
            film.PosterMin = entity.Posters.FirstOrDefault()?.MinImage;
            film.PosterMax = entity.Posters.FirstOrDefault()?.MaxImage;
            film.Year = entity.Year;
            film.Name = entity.Name;
            film.Description = entity.Description;
            film.Id = entity.Id;
            film.User = entity.User;
            film.UserId = entity.UserId;
            film.DateCreate = entity.DateCreate;
            return film;
        }

        #endregion

        #region common methods
        public List<int> AgregateSelectedFilmCrewIds(FilmEditModel model)
        {
            List<int> selectedfilmCrewIds = new List<int>();
            foreach (var item in model.ProducerIds)
            {
                selectedfilmCrewIds.Add(item);
            }
            foreach (var item in model.ActorIds)
            {
                selectedfilmCrewIds.Add(item);
            }

            return selectedfilmCrewIds;
        }
        #endregion
    }
}
