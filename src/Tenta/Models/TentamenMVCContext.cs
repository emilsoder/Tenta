using Microsoft.EntityFrameworkCore;

namespace Tenta.Models
{
    public partial class TentamenMVCContext : DbContext
    {
        public virtual DbSet<Bok> Bok { get; set; }

        public TentamenMVCContext(DbContextOptions<TentamenMVCContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bok>(entity =>
            {
                entity.Property(e => e.BokId).HasColumnName("BokID");
                entity.Property(e => e.Forfattare).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.Ilager).HasColumnName("ILager");
                entity.Property(e => e.Titel).IsRequired().HasColumnType("varchar(50)");
            });
        }
    }
}