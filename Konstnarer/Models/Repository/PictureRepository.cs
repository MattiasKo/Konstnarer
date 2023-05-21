using Konstnarer.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Konstnarer.Models.Repository
{
    public class PictureRepository:IPictureRepository
    {
        private readonly AppDbContext _context;
        public PictureRepository(AppDbContext context)
        {
            _context = context;   
        }
        public IEnumerable<Picture> GetAllPictures
        {
            get
            {
                return _context.Pictures.ToList();
            }
        }

        public void CreateNewPicture(Picture picture)
        {

            _context.Pictures.Add(picture);
            _context.SaveChanges();
        }

        public void DeletePicture(int id)
        {
            var PictureToDelete = _context.Pictures.Find(id);
            _context.Pictures.Remove(PictureToDelete);
            _context.SaveChanges();

        }

        public void EditPicture(Picture pictures)
        {
            _context.Entry(pictures).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Picture GetPictureById(int pictureId)
        {
            return _context.Pictures.FirstOrDefault(p => p.Id == pictureId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
