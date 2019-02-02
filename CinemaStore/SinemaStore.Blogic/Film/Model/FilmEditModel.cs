using CinemaStore.Entities.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Blogic.Film.Model
{
    public class FilmEditModel : FilmEntity
    {
        public int[] CountryIds { get; set; }
        public int[] CategoryIds { get; set; }
        public int[] ProducerIds { get; set; }
        public int[] ActorIds { get; set; }

    }
}
