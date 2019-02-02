using CinemaStore.Blogic.Base;
using CinemaStore.Entities.Category;
using CinemaStore.Models.Navigation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaStore.Controllers
{
    public class WidgetController : BaseController
    {
        private IBlogic<CategoryEntity> categorySrv;
        public WidgetController(IBlogic<CategoryEntity> categorySrv)
        {
            this.categorySrv = categorySrv;
        }

        public PartialViewResult Navigation()
        {
            NavigationModel model = new NavigationModel();
            model.AuthUser = User.Identity.IsAuthenticated;

            if (model.AuthUser)
            {
                model.UserName = User.Identity.GetUserName();

                if (string.IsNullOrEmpty(model.UserName))
                {
                    model.UserName =
                        UserManager.GetEmail(User.Identity.GetUserId());
                }
            }

            model.Categories = categorySrv
                .GetEntries(new Blogic.Model.PaginationModel { CountOnPage = 99999 });

            return PartialView(model);
        }
       
    }
}