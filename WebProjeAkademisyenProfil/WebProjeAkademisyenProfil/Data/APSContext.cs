using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebProjeAkademisyenProfil.Models;

namespace WebProjeAkademisyenProfil.Data
{
    public class APSContext: DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Academician> Academicians { get; set; }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Info> Infos { get; set; }

        public DbSet<Membership> Memberships { get; set; }

        public DbSet<Notice> Notices { get; set; }  

        public DbSet<Book> Books { get; set; }

        public DbSet<Lesson> Lessons { get; set; }  

        public DbSet<Award> Awards { get; set; }

        public DbSet<Experience> Experiences { get; set; }

        public DbSet<Certificate> Certificates { get; set; }

        public DbSet<UserAdmin> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
