using CinemaStore.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Entities.Film
{
    [Table("FilmCrew")]
    public class FilmCrewEntity : BaseEntity
    {
        public FilmCrewEntity()
        {
            Films = new HashSet<FilmEntity>();
        }
        [MaxLength(55)]
        public string GivenName { get; set; }
        [MaxLength(55)]
        public string FamilyName { get; set; }
        [MaxLength(55)]
        public string Patronymic { get; set; }
        public int Group { get; set; }

        public virtual ICollection<FilmEntity> Films { get; set; }

    }
}
