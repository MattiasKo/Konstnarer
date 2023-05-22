namespace Konstnarer.Models.ViewModels
{
    public class DetailPictureAndComments
    {
        public Picture picture { get; set; }
        public IEnumerable<PicComment> comments { get; set; }
        public string usersComment { get; set; }
    }
}
