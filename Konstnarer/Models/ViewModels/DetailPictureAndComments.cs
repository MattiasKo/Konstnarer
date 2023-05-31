namespace Konstnarer.Models.ViewModels
{
    public class DetailPictureAndComments
    {
        public Picture Pictures { get; set; }
        public List<PicComment> pictureComments { get; set; }
        public List<User> Users { get; set; }
        public string usersComment { get; set; }
        public User Owner { get; set; }

    }
}
