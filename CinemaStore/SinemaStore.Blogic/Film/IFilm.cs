using CinemaStore.Entities.Film;
using CinemaStore.Blogic.Base;
using CinemaStore.Blogic.Film.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CinemaStore.Blogic.Model;

namespace CinemaStore.Blogic.Film
{
    public interface IFilm 
    {
        FilmModel this[int id] { get; }
        FilmEntity Edit(FilmEditModel model);      
        List<FilmModel> GetFilms(PaginationModel model, Func<FilmEntity, bool> predicate = null);
        List<FilmModel> GetByCategory(PaginationModel model, int categoryId);        
    }
}
