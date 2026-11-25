using Microsoft.EntityFrameworkCore;
using ExamenCendres.Models;

namespace ExamenCendres.Data;

public class ExamenCendresContext : DbContext
{
    public DbSet<Zona> Zona { get; set; }
    public DbSet<Foguera> Foguera { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=ExamenCendres.db")
            .UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Zona>()
            .HasOne(z => z.Foguera)
            .WithOne(f => f.Zona)
            .HasForeignKey<Zona>("FogueraID")
            .IsRequired();
  
        modelBuilder.Entity<Zona>()
            .HasOne(z => z.Regio)
            .WithMany(r => r.Subzones)
            .HasForeignKey("RegioID")          
            .IsRequired(false)                 
            .OnDelete(DeleteBehavior.SetNull); 
    }
}