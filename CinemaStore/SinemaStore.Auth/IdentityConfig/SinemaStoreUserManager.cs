using CinemaStore.Db.Context;
using CinemaStore.Entities.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Auth.IdentityConfig
{
    public class CinemaStoreUserManager : UserManager<CinemaStoreUser>
    {
        public CinemaStoreUserManager(IUserStore<CinemaStoreUser> userStore) : base(userStore)
        { }


        public static CinemaStoreUserManager Get(IdentityFactoryOptions<CinemaStoreUserManager> opts, IOwinContext owinContext)
        {           
            var manager = new CinemaStoreUserManager(new UserStore<CinemaStoreUser>(new CinemaStoreContext()));

            manager.UserValidator = new UserValidator<CinemaStoreUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireUppercase = true,
                RequireLowercase = true
            };

            if (opts.DataProtectionProvider != null)
            {
                var protecotor = opts.DataProtectionProvider.Create("ASP.NET Identity");
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<CinemaStoreUser>(protecotor);
            }

            return manager;
        }
    }
}
