using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaStore.Db.Context;
using CinemaStore.Entities.Country;
using CinemaStore.Blogic.Base;
using CinemaStore.Blogic.Model;

namespace CinemaStore.Blogic.Country
{
    public class Country : IBlogic<CountryEntity>
    {
        private CinemaStoreContext context;

        public CountryEntity this[int id] => Get(id);

        public List<CountryEntity> Entries => Get();

        public void Delete(CountryEntity entity)
        {
            using (context = new CinemaStoreContext())
            {
                var country = context
                    .Country.FirstOrDefault(x => x.Id == entity.Id);
                if (country != null)
                {
                    context.Country.Remove(country);
                }

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

        public CountryEntity Edit(CountryEntity entity)
        {
            CountryEntity country = null;
            using (context = new CinemaStoreContext())
            {
                if (entity.Id > 0)
                {
                    country = context.Country.FirstOrDefault(x => x.Id == entity.Id);
                }
                else
                {
                    country = new CountryEntity();
                    context.Country.Add(country);
                }

                country.Name = entity.Name;
                country.Description = entity.Description;

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)//sql exceptions, object disposed, invalid operation etc...
                {
                    throw ex;
                }
            }

            return country;

        }

        public List<CountryEntity> GetEntries(PaginationModel model, Func<CountryEntity, bool> expr = null)
        {
            List<CountryEntity> countries = null;

            using (context = new CinemaStoreContext())
            {
                var query = context
                    .Country.AsQueryable();

                model.TotalItems = query.Count();

                if (expr != null)
                {
                    query = query.Where(expr).AsQueryable();
                }

                countries = query.OrderBy(x => x.DateCreate)
                    .Skip((model.Page - 1) * model.Page).Take(model.CountOnPage)
                    .ToList();
            }

            return countries;
        }

        #region common getters

        private CountryEntity Get(int id)
        {
            CountryEntity entity = new CountryEntity();
            using (context = new CinemaStoreContext())
            {
                entity = context
                    .Country.FirstOrDefault(x => x.Id == id);
            }
            return entity;
        }

        private List<CountryEntity> Get()
        {
            List<CountryEntity> countries = null;

            using (context = new CinemaStoreContext())
            {
                countries = context
                    .Country.ToList();
            }

            return countries;

        }

        #endregion
    }
}
