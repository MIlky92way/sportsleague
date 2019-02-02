using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using CinemaStore.Auth.IdentityConfig;

[assembly: OwinStartup(typeof(CinemaStore.Startup))]

namespace CinemaStore
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {            
            ConfigureAuth(app);
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<CinemaStoreUserManager>(CinemaStoreUserManager.Get);

            app.CreatePerOwinContext<CinemaStoreSignInManager>(CinemaStoreSignInManager.CreateSignInManager);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieName = "_pw_m",
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = new TimeSpan(8, 0, 0),
            });
        }
    }
}
