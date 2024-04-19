using Key_Management_System.Models;

namespace Key_Management_System.Models
{
    public class Worker: User
    {
        public string Faculty { get; set; } = string.Empty;

        public ICollection<RequestKey>? AssignKeys { get; set; }
    }
}
