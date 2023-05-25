namespace Konstnarer.Models.ViewModels
{
    public class ViewProfileModel
    {
        public List<Picture> UsersPictures { get; set; }
        public List<ProfileComment> comments { get; set; }
        public User ProfileUser { get; set; }
        public List<Guid> UserIdComment {get;set; }
        public List<User> users {get;set; }
        public string usersComment { get; set; }

    }
}
