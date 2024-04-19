namespace Key_Management_System.Models
{
    public class IBaseEntity
    {
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
