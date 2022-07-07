using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShareLikesEF.Data
{
    public class ImageRepository
    {
        private string _connectionString;
        public ImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(Image image)
        {
            using var context = new ImageDataContext(_connectionString);
            context.Images.Add(image);
            context.SaveChanges();
        }
        public List<Image> GetAll()
        {
            using var context = new ImageDataContext(_connectionString);
            return context.Images.OrderByDescending(i=>i.DateUploaded).ToList();
        }
        public Image GetImageForId(int id)
        {
            using var context = new ImageDataContext(_connectionString);
            return context.Images.FirstOrDefault(i => i.Id == id);
        }
        public int GetLikes(int id)
        {
            using var context = new ImageDataContext(_connectionString);
            var image= context.Images.FirstOrDefault(i => i.Id == id);
            return image.NumberOfLikes;

        }
        public void LikeIt(int id)
        {
            using var context = new ImageDataContext(_connectionString);
            var image = context.Images.FirstOrDefault(i => i.Id == id);
            image.NumberOfLikes++;
            context.SaveChanges();
        }
    }
}
