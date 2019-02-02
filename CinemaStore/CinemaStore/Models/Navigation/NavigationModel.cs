using CinemaStore.Entities.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaStore.Models.Navigation
{
    public class NavigationModel
    {

        public bool AuthUser { get; set; }
        public string UserName { get; set; }

        public IEnumerable<CategoryEntity> Categories { get; set; }
        
    }
}