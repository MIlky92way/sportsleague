using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Entities.Auth
{
    public class CinemaStoreUser:IdentityUser
    {
        public int? Age { get; set; }
        public DateTime? BirthDate { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<CinemaStoreUser> userManager)
        {
            var userIdentity =
                await
                userManager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}
