using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaStore.Db.Context;
using CinemaStore.Entities.Film;
using CinemaStore.Blogic.Model;

namespace CinemaStore.Blogic.FilmCrew
{
    public class FilmCrew : IFilmCrew
    {
        private CinemaStoreContext context;
        public FilmCrewEntity this[int id] => GetBy(id);

        public List<FilmCrewEntity> Entries => Get();

        public void Delete(FilmCrewEntity entity)
        {
            using (context = new CinemaStoreContext())
            {
                var entry = context.FilmCrew.FirstOrDefault(x => x.Id == entity.Id);
                context.FilmCrew.Remove(entry);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }

        public FilmCrewEntity Edit(FilmCrewEntity entity)
        {
            FilmCrewEntity entry = null;
            using (context = new CinemaStoreContext())
            {
                if (entity.Id > 0)
                {
                    entry = context.FilmCrew.FirstOrDefault(x => x.Id == entity.Id);
                }
                else
                {
                    entry = new FilmCrewEntity();
                    context.FilmCrew.Add(entry);
                }

                entry.FamilyName = entity.FamilyName;
                entry.GivenName = entity.GivenName;
                entry.Patronymic = entity.Patronymic;
                entry.Group = entity.Group;

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return entry;
        }

        public List<FilmCrewEntity> Get(int group)
        {
            List<FilmCrewEntity> filmCrew = null;
            using (context = new CinemaStoreContext())
            {
                filmCrew = context.FilmCrew.ToList();
            }
            return filmCrew;
        }

        public List<FilmCrewEntity> GetEntries(PaginationModel model, Func<FilmCrewEntity, bool> expr = null)
        {
            List<FilmCrewEntity> filmCrew = null;
            using (context = new CinemaStoreContext())
            {
                var query = context.FilmCrew.AsQueryable();

                model.TotalItems = query.Count();

                if (expr != null)
                {
                    query = query.Where(expr).AsQueryable();
                }

                filmCrew = query
                    .OrderBy(x => x.DateCreate)
                    .Skip((model.Page - 1) * model.CountOnPage).Take(model.CountOnPage)
                    .ToList();
            }
            return filmCrew;
        }

        #region common getters

        private FilmCrewEntity GetBy(int id)
        {
            FilmCrewEntity entity = null;
            using (context = new CinemaStoreContext())
            {
                entity = context.FilmCrew.FirstOrDefault(x => x.Id == id);
            }
            return entity;
        }
        private List<FilmCrewEntity> Get()
        {
            List<FilmCrewEntity> result = null;
            using (context = new CinemaStoreContext())
            {
                result = context.FilmCrew.ToList();
            }
            return result;
        }
        #endregion
    }
}
