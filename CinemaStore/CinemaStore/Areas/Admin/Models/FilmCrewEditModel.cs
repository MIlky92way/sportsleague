using CinemaStore.Entities.Film;
using CinemaStore.Blogic.FilmCrew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaStore.Areas.Admin.Models
{
    public class FilmCrewEditModel:FilmCrewEntity
    {
        public IEnumerable<SelectListItem> SelectFilmCrewGroup
        {
            get
            {
                var vals = Enum.GetValues(typeof(FilmCrewGroup));
                foreach (object item in vals)
                {
                    yield return new SelectListItem
                    {
                        Text = Enum.GetName(typeof(FilmCrewGroup), item),
                        Value = ((int)item).ToString()
                    };
                }
            }
        }
    }
}