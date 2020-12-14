using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public virtual AppDataUser User { get; set; }
    }
    public class AppRole : IdentityRole { }
    public class AppUserRole : IdentityUserRole<string> { }
    public class AppUserClaim : IdentityUserClaim<string> { }
    public class AppRoleClaim : IdentityRoleClaim<string> { }
    public class AppUserLogin : IdentityUserLogin<string> { }
    public class AppUserToken : IdentityUserToken<string> { }

    public class AppUserRefreshToken
    {
        [Key]
        [ForeignKey("AppUser")]
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;

        public virtual AppUser AppUser { get; set; }
    }
}
