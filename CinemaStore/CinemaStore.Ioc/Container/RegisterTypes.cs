using Autofac;
using CinemaStore.Entities.Category;
using CinemaStore.Entities.Country;
using CinemaStore.Blogic.Base;
using CinemaStore.Blogic.Category;
using CinemaStore.Blogic.Country;
using CinemaStore.Blogic.Film;
using CinemaStore.Blogic.FilmCrew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaStore.Entities.Film;
using CinemaStore.Blogic.Poster;

namespace CinemaStore.Ioc.Container
{
    public class RegisterTypes
    {
        public void InitDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<Country>().As<IBlogic<CountryEntity>>();
            builder.RegisterType<Category>().As<IBlogic<CategoryEntity>>();
            builder.RegisterType<FilmCrew>().As<IFilmCrew>();
                        
            builder.RegisterAssemblyTypes(typeof(Film).Assembly)
                .AsImplementedInterfaces();

            builder.RegisterType<Poster>().As<IPoster>();
        }
    }
}
