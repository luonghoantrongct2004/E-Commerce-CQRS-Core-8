using E.Domain.Entities.Users;

namespace E.Domain.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BasicUser>? Users { get; set; }
    }

}
