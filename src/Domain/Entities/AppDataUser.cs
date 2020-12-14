using System.Collections.Generic;

namespace Domain.Entities
{
    public class AppDataUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual AppUser ApplicationUser { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
