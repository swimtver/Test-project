using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoUploader.Data
{
    public class PhotoService
    {
        readonly PhotoContext context = new PhotoContext();

        public Photo Upload(Photo photo)
        {
            context.Photoes.Add(photo);
            context.SaveChanges();
            return photo;
        }

        public void Delete(Photo photo)
        {
            context.Photoes.Remove(photo);
            context.SaveChanges();
        }

        public Photo GetById(int id)
        {
            return context.Photoes.Find(id);
        }

        public IEnumerable<Photo> GetPhotoesList()
        {
            return context.Photoes.OrderByDescending(p => p.Id).ToList(); // Последние добавленные сначала.
        }

        public IEnumerable<Photo> GetPhotoesList(int from, int count)
        {
            return context.Photoes.Where(p => p.Id < from).OrderByDescending(p => p.Id).Take(count).ToList();
        }
    }
}
