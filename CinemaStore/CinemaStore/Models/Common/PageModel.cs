using CinemaStore.Blogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaStore.Models.Common
{
    public class PageModel : PaginationModel
    {

        public string ClassName { get; set; }

        public IEnumerable<SelectListItem> SelectCountIitemsOnPage
        {
            get
            {
                foreach (int item in CountOnPageArray)
                {
                    yield return new SelectListItem
                    {
                        Text = item.ToString(),
                        Value = item.ToString(),
                        Selected = item == CountOnPage,
                    };
                }
            }
        }


    }
}