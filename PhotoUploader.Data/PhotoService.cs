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
            context.Photos.Add(photo);
            context.SaveChanges();
            return photo;
        }

        public void Delete(Photo photo)
        {
            context.Photos.Remove(photo);
            context.SaveChanges();
        }

        public Photo GetById(int id)
        {
            return context.Photos.Find(id);
        }

        public IEnumerable<Photo> GetPhotoesList()
        {
            return context.Photos.OrderByDescending(p => p.Id).ToList(); // Последние добавленные сначала.
        }

        public IEnumerable<Photo> GetFakePhotoesList()
        {
            var item = GetById(1);
            for (int i = 0; i < 12; i++)
            {
                yield return item;
            }
        }

        public IEnumerable<Photo> GetPhotoesList(int from, int count)
        {
            return context.Photos.Where(p => p.Id < from).OrderByDescending(p => p.Id).Take(count).ToList();
        }

        public int Count()
        {
           return context.Photos.Count();
        }        
    }
}
