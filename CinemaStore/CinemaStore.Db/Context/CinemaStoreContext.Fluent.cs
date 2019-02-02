using CinemaStore.Entities.Category;
using CinemaStore.Entities.Country;
using CinemaStore.Entities.Film;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Db.Context
{
   public partial class CinemaStoreContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmEntity>()
                .HasMany<CountryEntity>(film => film.Countries)
                .WithMany(country => country.Films)
                .Map(fc =>
                {
                    fc.MapLeftKey("FilmId");
                    fc.MapRightKey("CountryId");
                    fc.ToTable("FilmCountry");
                });

            modelBuilder.Entity<FilmEntity>()
                .HasMany<CategoryEntity>(film => film.Categories)
                .WithMany(category => category.Films)
                .Map(fc =>
                {
                    fc.MapLeftKey("FilmId");
                    fc.MapRightKey("CategoryId");
                    fc.ToTable("FilmCategory");
                });

            modelBuilder.Entity<FilmEntity>()
                .HasMany<FilmCrewEntity>(film => film.FilmCrew)
                .WithMany(crew => crew.Films)
                .Map(fc =>
                {
                    fc.MapLeftKey("FilmId");
                    fc.MapRightKey("FilmCrewId");
                    fc.ToTable("Film_FilmCrew");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
