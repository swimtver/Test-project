using System;
using System.Linq;
using System.Data.Entity;

namespace PhotoUploader.Data
{
    public class PhotoContext : DbContext
    {
        public IDbSet<Photo> Photoes { get; set; }

        public PhotoContext() : base("photoBase") { Database.SetInitializer(new DropCreateDatabaseAlways<PhotoContext>()); }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetupPhotoEntity(modelBuilder);
        }

        private void SetupPhotoEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>().ToTable("Photoes");
            modelBuilder.Entity<Photo>().HasKey(a => a.Id);
            modelBuilder.Entity<Photo>().Property(a => a.Id).HasColumnName("Id");
            modelBuilder.Entity<Photo>().Property(a => a.FileName).HasColumnName("FileName");
            modelBuilder.Entity<Photo>().Property(a => a.FileExtention).HasColumnName("FileExtention").HasMaxLength(5);
            modelBuilder.Entity<Photo>().Property(a => a.ContentType).HasColumnName("ContentType").HasMaxLength(10).IsOptional();
            modelBuilder.Entity<Photo>().Property(a => a.FileSize).HasColumnName("FileSize");
            modelBuilder.Entity<Photo>().Property(a => a.UploadDate).HasColumnName("UploadDate");
            modelBuilder.Entity<Photo>().Property(a => a.Content).HasColumnName("Content");
        }
    }
}
