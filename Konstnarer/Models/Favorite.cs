namespace Konstnarer.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int PictureId { get; set; }
        public Guid UserId { get; set; }
    }
}
