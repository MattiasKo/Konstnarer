namespace Konstnarer.Models
{
    public class UserLogin
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
