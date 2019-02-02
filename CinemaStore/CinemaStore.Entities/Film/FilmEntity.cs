using CinemaStore.Entities.Auth;
using CinemaStore.Entities.Base;
using CinemaStore.Entities.Category;
using CinemaStore.Entities.Country;
using CinemaStore.Entities.PosterImage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Entities.Film
{
    [Table("Film")]
    public class FilmEntity : BaseEntity
    {
        public FilmEntity()
        {
            FilmCrew = new HashSet<FilmCrewEntity>();
            Categories = new HashSet<CategoryEntity>();
            Posters = new HashSet<PosterImageEntity>();
            Countries = new HashSet<CountryEntity>();
        }

        [MaxLength(75)]
        public string Name { get; set; }
        public int AgeRestrictions { get; set; }
        public double IMDb { get; set; }
        public int TotalViewTime { get; set; }
        public int Year { get; set; }

        [MaxLength(520)]
        public string Description { get; set; }

        [MaxLength(128)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public CinemaStoreUser User { get; set; }
        public virtual ICollection<FilmCrewEntity> FilmCrew { get; set; }
        public virtual ICollection<CategoryEntity> Categories { get; set; }        
        public virtual ICollection<CountryEntity> Countries { get; set; }
        public virtual ICollection<PosterImageEntity> Posters { get; set; }
    }
}
