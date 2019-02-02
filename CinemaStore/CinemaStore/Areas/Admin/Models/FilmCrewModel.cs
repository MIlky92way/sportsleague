using CinemaStore.Entities.Film;
using CinemaStore.Models.Common;
using CinemaStore.Blogic.FilmCrew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaStore.Areas.Admin.Models
{
    public class FilmCrewModel : PageModel
    {
        public IEnumerable<FilmCrewEntity> FilmCrew { get; set; }

        
    }
}