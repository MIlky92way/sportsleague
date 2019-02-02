using CinemaStore.Db.Context;
using CinemaStore.Entities.Category;
using CinemaStore.Entities.Country;
using CinemaStore.Entities.Film;
using CinemaStore.Entities.PosterImage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Db.Migrations
{
    public class Configuration : DbMigrationsConfiguration<CinemaStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(CinemaStoreContext context)
        {
            base.Seed(context);
        }

   

    }
}
