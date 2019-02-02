using CinemaStore.Entities.Category;
using CinemaStore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaStore.Areas.Admin.Models
{
    public class CategoryModel:PageModel
    {
        public IEnumerable <CategoryEntity> Categories { get; set; }
    }
}