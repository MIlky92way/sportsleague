using CinemaStore.Db.Migrations;
using CinemaStore.Entities.Auth;
using CinemaStore.Entities.Category;
using CinemaStore.Entities.Country;
using CinemaStore.Entities.Film;
using CinemaStore.Entities.PosterImage;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Db.Context
{
    public partial class CinemaStoreContext:IdentityDbContext<CinemaStoreUser>
    {
        public CinemaStoreContext():base("SinemaStoreConnection")
        {
            Database.SetInitializer<CinemaStoreContext>(new MigrateDatabaseToLatestVersion<CinemaStoreContext, Configuration>());
        }
        public DbSet<CategoryEntity> Category { get; set; }
        public DbSet<CountryEntity> Country { get; set; }
        public DbSet<FilmCrewEntity> FilmCrew { get; set; }
        public DbSet<FilmEntity> FilmEntity { get; set; }
        public DbSet<PosterImageEntity> PosterImage { get; set; }
    }
}
