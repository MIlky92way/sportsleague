using CinemaStore.Entities.Country;
using CinemaStore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaStore.Areas.Admin.Models
{
    public class CountryModel : PageModel
    {
        public IEnumerable<CountryEntity> Countries { get; set; }
    }
}