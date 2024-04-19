namespace Key_Management_System.Models
{
    public class LogoutToken: IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Guid Identifier { get; set; }
    }
}
