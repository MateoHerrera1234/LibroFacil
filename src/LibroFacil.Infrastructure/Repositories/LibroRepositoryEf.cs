using LibroFacil.Application.Interfaces;
using LibroFacil.Domain.Entities;
using LibroFacil.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibroFacil.Infrastructure.Repositories;

public class LibroRepositoryEf : ILibroRepository
{
    private readonly LibroFacilDbContext _context;

    public LibroRepositoryEf(LibroFacilDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Libro>> GetAllAsync() => await _context.Libros.ToListAsync();

    public async Task<Libro?> GetByIdAsync(int id) => await _context.Libros.FindAsync(id);

    public async Task<Libro?> GetByIsbnAsync(string isbn) =>
        await _context.Libros.FirstOrDefaultAsync(l => l.ISBN == isbn);

    public async Task AddAsync(Libro libro)
    {
        await _context.Libros.AddAsync(libro);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Libro libro)
    {
        _context.Libros.Update(libro);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var libro = await GetByIdAsync(id);
        if (libro != null)
        {
            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
        }
    }
}