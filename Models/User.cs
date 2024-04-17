using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
