using CinemaStore.Entities.Base;
using CinemaStore.Entities.Film;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Entities.PosterImage
{
    [Table("PosterImage")]
    public class PosterImageEntity : BaseEntity
    {        
        [MaxLength(128)]
        public string MaxImage { get; set; }
        [MaxLength(128)]
        public string MinImage { get; set; }

        public int FilmId { get; set; }

        [ForeignKey("FilmId")]
        public FilmEntity Film { get; set; } 
    }
}
