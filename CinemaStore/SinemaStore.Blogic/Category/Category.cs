using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaStore.Db.Context;
using CinemaStore.Entities.Category;
using CinemaStore.Blogic.Base;
using CinemaStore.Blogic.Model;

namespace CinemaStore.Blogic.Category
{
    public class Category : IBlogic<CategoryEntity>
    {
        private CinemaStoreContext context;
        public CategoryEntity this[int id] => Get(id);

        public List<CategoryEntity> Categories => Get();

        public List<CategoryEntity> Entries => Get();

        public void Delete(CategoryEntity entity)
        {
            using (context = new CinemaStoreContext())
            {
                var category = context
                    .Category.FirstOrDefault(x => x.Id == entity.Id);
                if (category != null)
                {
                    context.Category.Remove(category);
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

        public CategoryEntity Edit(CategoryEntity entity)
        {
            CategoryEntity category = null;
            using (context = new CinemaStoreContext())
            {
                if (entity.Id > 0)
                {
                    category = context.Category.FirstOrDefault(x => x.Id == entity.Id);
                }
                else
                {
                    category = new CategoryEntity();
                    context.Category.Add(category);
                }

                category.Name = entity.Name;
                category.Alias = entity.Alias;
                category.Description = entity.Description;

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)//sql exceptions, object disposed, invalid operation etc...
                {
                    throw ex;
                }
            }

            return category;
        }

        public List<CategoryEntity> GetEntries(PaginationModel model, Func<CategoryEntity, bool> expr = null)
        {
            List<CategoryEntity> categories = null;

            using (context = new CinemaStoreContext())
            {
                var query = context
                    .Category.AsQueryable();

                model.TotalItems = query.Count();

                if (expr != null)
                {
                    query = query.Where(expr).AsQueryable();
                }

                categories = query.OrderBy(x => x.DateCreate)
                    .Skip((model.Page - 1) * model.CountOnPage).Take(model.CountOnPage)
                    .ToList();
            }

            return categories;
        }

        #region common getters

        private CategoryEntity Get(int id)
        {
            CategoryEntity entity = new CategoryEntity();
            using (context = new CinemaStoreContext())
            {
                entity = context
                    .Category.FirstOrDefault(x => x.Id == id);
            }
            return entity;
        }

        private List<CategoryEntity> Get()
        {
            List<CategoryEntity> categories = null;

            using (context = new CinemaStoreContext())
            {
                categories = context
                    .Category.ToList();
            }

            return categories;

        }

        #endregion
    }
}
