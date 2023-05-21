namespace Konstnarer.Models.Interfaces
{
    public interface IPictureRepository
    {
        IEnumerable<Picture> GetAllPictures { get; }
        Picture GetPictureById(int pictureId);
        void DeletePicture(int id);
        void EditPicture(Picture picture);
        public void CreateNewPicture(Picture picture);

        public Task SaveAsync();
    }
}
