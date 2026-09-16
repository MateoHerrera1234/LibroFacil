using LibroFacil.Application.Interfaces;
using LibroFacil.Domain.Entities;

namespace LibroFacil.Application.Services;

public class LibroService
{
    private readonly ILibroRepository _repository;

    public LibroService(ILibroRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Libro>> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<Libro?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task AddAsync(Libro libro)
    {
        await ValidarLibroAsync(libro, esNuevo: true);
        await _repository.AddAsync(libro);
    }

    public async Task UpdateAsync(int id, Libro libroActualizado)
    {
        var libroExistente = await _repository.GetByIdAsync(id);
        if (libroExistente == null)
            throw new ArgumentException("El libro no existe");

        await ValidarLibroAsync(libroActualizado, esNuevo: false, idActual: id);

        libroExistente.ISBN = libroActualizado.ISBN;
        libroExistente.Titulo = libroActualizado.Titulo;
        libroExistente.Autor = libroActualizado.Autor;
        libroExistente.AnioPublicacion = libroActualizado.AnioPublicacion;
        libroExistente.Stock = libroActualizado.Stock;

        await _repository.UpdateAsync(libroExistente);
    }

    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

    private async Task ValidarLibroAsync(Libro libro, bool esNuevo, int idActual = 0)
    {
        if (string.IsNullOrWhiteSpace(libro.ISBN))
            throw new ArgumentException("El ISBN es obligatorio");

        if (string.IsNullOrWhiteSpace(libro.Titulo))
            throw new ArgumentException("El título es obligatorio");

        if (string.IsNullOrWhiteSpace(libro.Autor))
            throw new ArgumentException("El autor es obligatorio");

        if (libro.AnioPublicacion <= 0 || libro.AnioPublicacion > DateTime.Now.Year)
            throw new ArgumentException($"El año de publicación debe ser mayor a 0 y no mayor al año actual ({DateTime.Now.Year})");

        if (libro.Stock < 0)
            throw new ArgumentException("El stock no puede ser negativo.");

        var libroExistenteConIsbn = await _repository.GetByIsbnAsync(libro.ISBN);
        if (libroExistenteConIsbn != null && (esNuevo || libroExistenteConIsbn.Id != idActual))
            throw new InvalidOperationException("El ISBN ya existe en el sistema");
    }
}