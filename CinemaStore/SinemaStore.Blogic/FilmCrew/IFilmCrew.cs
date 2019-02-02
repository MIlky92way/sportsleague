using CinemaStore.Entities.Film;
using CinemaStore.Blogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Blogic.FilmCrew
{
    public interface IFilmCrew:IBlogic<FilmCrewEntity>
    {        
        List<FilmCrewEntity> Get(int group);
        
    }
}
