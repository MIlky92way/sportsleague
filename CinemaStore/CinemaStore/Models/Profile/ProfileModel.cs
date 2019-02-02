using CinemaStore.Blogic.Film.Model;
using CinemaStore.Entities.Film;
using CinemaStore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaStore.Models.Profile
{
    public class ProfileModel:PageModel
    {
        public int Age { get; set; }
        public string GivenName { get; set; }
        public string Email { get; set; }

        public IEnumerable<FilmModel> Films { get; set; }
    }
}