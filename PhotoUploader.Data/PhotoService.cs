using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

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
                
        public Photo GetByIdIncludeOriginal(int id)
        {
            return context.Photos.Include(p => p.Original).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Photo> GetPhotoesList()
        {
            return context.Photos.OrderByDescending(p => p.Id).ToList(); // Последние добавленные сначала.
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
