using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Entities.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            DateCreate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
