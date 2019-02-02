using CinemaStore.Blogic.Film.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaStore.Models.Film
{
    public class FilmEditViewModel:FilmEditModel
    {
        
        public HttpPostedFileBase[] Files { get; set; }
    }
}