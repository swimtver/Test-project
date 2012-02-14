using System;
using System.Linq;
using System.Data.Entity;

namespace PhotoUploader.Data
{
    public class PhotoContext : DbContext
    {
        public IDbSet<Photo> Photos { get; set; }
        public IDbSet<Original> Originals { get; set; }        

        public PhotoContext() : base("photoBase") {
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetupPhotoEntity(modelBuilder);
        }

        private void SetupPhotoEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>().ToTable("Photos");
            modelBuilder.Entity<Photo>().HasKey(a => a.Id);
            modelBuilder.Entity<Photo>().Property(a => a.Id).HasColumnName("Id");
            modelBuilder.Entity<Photo>().Property(a => a.FileName).HasColumnName("FileName");
            modelBuilder.Entity<Photo>().Property(a => a.FileExtention).HasColumnName("FileExtention").HasMaxLength(5);
            modelBuilder.Entity<Photo>().Property(a => a.ContentType).HasColumnName("ContentType").HasMaxLength(10).IsOptional();
            modelBuilder.Entity<Photo>().Property(a => a.FileSize).HasColumnName("FileSize");
            modelBuilder.Entity<Photo>().Property(a => a.UploadDate).HasColumnName("UploadDate");
            
            modelBuilder.Entity<Original>().ToTable("Photos");
            modelBuilder.Entity<Original>().HasKey(a => a.Id);
            modelBuilder.Entity<Original>().Property(a => a.Id).HasColumnName("Id");
            modelBuilder.Entity<Original>().Property(a => a.Content).HasColumnName("OriginalContent");

            modelBuilder.Entity<Photo>().HasRequired(p => p.Original).WithRequiredPrincipal();           
        }
    }
}
