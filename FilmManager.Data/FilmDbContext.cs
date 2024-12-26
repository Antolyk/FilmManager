using FilmManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmManager.Data
{
    public class FilmDbContext : DbContext
    {
        public DbSet<Film> Films { get; set; }

        public FilmDbContext(DbContextOptions<FilmDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //The comments below should be removed when it is necessary to perform unit tests
            //if (!optionsBuilder.IsConfigured)
            //{
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=FilmManagerDB;Trusted_Connection=true;TrustServerCertificate=true;");
            //}
        }
    }
}
