using LibroFacil.Domain.Entities;

namespace LibroFacil.Application.Interfaces;

public interface ILibroRepository
{
    Task<IEnumerable<Libro>> GetAllAsync();
    Task<Libro?> GetByIdAsync(int id);
    Task<Libro?> GetByIsbnAsync(string isbn);
    Task AddAsync(Libro libro);
    Task UpdateAsync(Libro libro);
    Task DeleteAsync(int id);
}