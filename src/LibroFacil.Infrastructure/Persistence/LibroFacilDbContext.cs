using LibroFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibroFacil.Infrastructure.Persistence;

public class LibroFacilDbContext : DbContext
{
    public LibroFacilDbContext(DbContextOptions<LibroFacilDbContext> options) : base(options) { }

    public DbSet<Libro> Libros => Set<Libro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ISBN).IsUnique();
            entity.Property(e => e.Titulo).IsRequired();
            entity.Property(e => e.Autor).IsRequired();
        });
    }
}