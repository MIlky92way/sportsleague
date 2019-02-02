using CinemaStore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CinemaStore.Helpers
{
    public static class PaginationHelper
    {
        public static MvcHtmlString Pagination(this HtmlHelper helper, PageModel model, Func<int, string> url)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= model.Total; i++)
            {
                TagBuilder a = new TagBuilder("a");
                a.SetInnerText(i.ToString());
                a.MergeAttribute("href", url(i));
                if (i == model.Page)
                {
                    a.AddCssClass($"{model.ClassName} active");
                }
                a.AddCssClass(model.ClassName);
                result.Append(a.ToString());
            }

            return new MvcHtmlString(result.ToString());
        }
    }
}