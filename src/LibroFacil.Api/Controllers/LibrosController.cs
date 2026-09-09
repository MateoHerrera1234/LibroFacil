using LibroFacil.Application.Services;
using LibroFacil.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibroFacil.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibrosController : ControllerBase
{
    private readonly LibroService _libroService;

    public LibrosController(LibroService libroService)
    {
        _libroService = libroService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _libroService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var libro = await _libroService.GetByIdAsync(id);
        if (libro == null) return NotFound(new { mensaje = "Libro no encontrado." });
        return Ok(libro);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Libro libro)
    {
        try
        {
            await _libroService.AddAsync(libro);
            return CreatedAtAction(nameof(GetById), new { id = libro.Id }, libro);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Libro libro)
    {
        try
        {
            await _libroService.UpdateAsync(id, libro);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _libroService.DeleteAsync(id);
        return NoContent();
    }
}