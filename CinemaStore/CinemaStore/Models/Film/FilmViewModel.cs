using CinemaStore.Blogic.Category;
using CinemaStore.Blogic.Film.Model;
using CinemaStore.Blogic.Model;
using CinemaStore.Entities.Category;
using CinemaStore.Entities.Film;
using CinemaStore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaStore.Models.Film
{
    public class FilmViewModel:PageModel
    {
        public int id { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<FilmModel> Films { get; set; }
    }
}