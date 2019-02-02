using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaStore.Db.Context;
using CinemaStore.Entities.PosterImage;

namespace CinemaStore.Blogic.Poster
{
    public class Poster : IPoster
    {
        private CinemaStoreContext context;
        public List<PosterImageEntity> this[int filmId]
        {
            get
            {
                List<PosterImageEntity> images = null;
                if (filmId == 0)
                {
                    return images;
                }
                using (context = new CinemaStoreContext())
                {
                    images = context
                        .PosterImage
                        .Where(x => x.FilmId == filmId)
                        .ToList();
                }
                return images;
            }
        }


        public void Add(PosterImageEntity image)
        {
            if (image == null)
            {
                return;
            }

            using (context = new CinemaStoreContext())
            {
                context.PosterImage.Add(image);

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

        public void Delete(PosterImageEntity image)
        {
            using (context = new CinemaStoreContext())
            {
                try
                {
                    context.Entry<PosterImageEntity>(image).State = System.Data.Entity.EntityState.Deleted;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                

               

                context.SaveChanges();
            }
        }

        public void Delete(List<PosterImageEntity> images)
        {
            foreach (var img in images ?? new List<PosterImageEntity>())
            {
                Delete(img);
            }
        }
    }
}
