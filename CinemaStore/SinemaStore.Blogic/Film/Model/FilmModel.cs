using CinemaStore.Entities.Auth;
using CinemaStore.Entities.Category;
using CinemaStore.Entities.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Blogic.Film.Model
{
    public class FilmModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public DateTime DateCreate { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Path to poster for preview
        /// </summary>
        public string PosterMin { get; set; }
        /// <summary>
        /// Path to poster for detail page
        /// </summary>
        public string PosterMax { get; set; }
        public List<FilmCrewEntity> Actors { get; set; }
        public List<FilmCrewEntity> Producer { get; set; }
        public List<CategoryEntity> Categories { get; set; }
        public CinemaStoreUser User { get; set; }
        public string UserId { get; set; }
    }
}
