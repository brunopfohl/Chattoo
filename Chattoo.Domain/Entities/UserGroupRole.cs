namespace Chattoo.Domain.Entities
{
    public class UserGroup
    {
        public string UserId { get; set; }
        
        public string RoleId { get; set; }
        
        public virtual User User { get; set; }
        
        public virtual GroupRole Role { get; set; }
    }
}