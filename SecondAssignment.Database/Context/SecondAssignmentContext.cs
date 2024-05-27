
using Microsoft.EntityFrameworkCore;
using SecondAssignment.Database.Entities;

namespace SecondAssignment.Database.Context
{
    public class SecondAssignmentContext : DbContext
    {
        public DbSet<Series> Series { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public SecondAssignmentContext(DbContextOptions<SecondAssignmentContext> options) : base(options)
        {


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-QCIUVPFJ;Database=SencondAssignment; user=sa; Password=Alejandro23@#;  TrustServerCertificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Series>(entity =>
            {
                entity.HasKey(s => s.SeriesId);
                entity.HasOne(s => s.PrimaryGenre).WithMany()
                .HasForeignKey(s => s.PrimaryGenreId)
                .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(s => s.PrimaryGenre)
                 .WithMany()
                 .HasForeignKey(s => s.PrimaryGenreId)
                .OnDelete(DeleteBehavior.NoAction);

               
                entity.HasOne(s => s.SecundaryGenre)
                      .WithMany()
                      .HasForeignKey(s => s.SecundaryGenreId)
                      .OnDelete(DeleteBehavior.NoAction);
            });


            //Todo: Fix This
            modelBuilder.Entity<Producer>(entity =>
            {
                entity.HasKey(p => p.ProducersId);

                entity.HasMany(p => p.Series)
                  .WithOne(p => p.Producer)
                  .HasForeignKey(p => p.ProducerId);
            });


            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(g => g.GenreId);
            });


        }
    }
}
