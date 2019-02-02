using CinemaStore.Entities.Auth;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Auth.IdentityConfig
{
    public class CinemaStoreSignInManager : SignInManager<CinemaStoreUser, string> 
    {
        public CinemaStoreSignInManager(CinemaStoreUserManager userManager, IAuthenticationManager authManager)
            : base(userManager, authManager)
        {

        }

        public ClaimsIdentity CreateUserIdentity(CinemaStoreUser user)
        {
            return user.GenerateUserIdentityAsync((CinemaStoreUserManager)UserManager).Result;
        }

        public static CinemaStoreSignInManager CreateSignInManager(IdentityFactoryOptions<CinemaStoreSignInManager> options, IOwinContext context)
        {
            return 
                new CinemaStoreSignInManager(context.GetUserManager<CinemaStoreUserManager>(), 
                context.Authentication);
        }
    }
}
