using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual User User { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }

    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<User> ClientUsers { get; set; }
    }
}
