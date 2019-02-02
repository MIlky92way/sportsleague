using CinemaStore.Entities.Base;
using CinemaStore.Entities.Film;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Entities.Country
{
    [Table("Country")]
    public class CountryEntity:BaseEntity
    {
        public CountryEntity()
        {
            Films = new HashSet<FilmEntity>();
        }
        
        [MaxLength(75)]
        public string Name { get; set; }
        
        [MaxLength(255)]
        public string Description { get; set; }
        
        public virtual ICollection<FilmEntity> Films { get; set; }
    }
}
