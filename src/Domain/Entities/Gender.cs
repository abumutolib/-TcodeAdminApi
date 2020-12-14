using System.Collections.Generic;

namespace Domain.Entities
{
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<AppDataUser> ClientUsers { get; set; }
    }
}
