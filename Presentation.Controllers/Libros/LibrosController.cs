using Business.DTOs;
using Business.Interfaces.Libros;
using Data.Access.Entities.Autores;
using Data.Access.Entities.Categorias;
using Data.Access.Entities.Libros;
using Data.Access.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers.Libros
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILibroService _libroService;

        public LibrosController(IUnitOfWork unitOfWork, ILibroService libroService)
        {
            _unitOfWork = unitOfWork;
            _libroService = libroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDTO>>> GetLibros()
        {
            var libros = await _unitOfWork.Libros.GetAllAsync("Autor,Categoria");

            var dto = libros.Select(l => new LibroDTO
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Descripcion = l.Descripcion,
                Precio = l.Precio,
                Disponible = l.Disponible,
                Autor = l.Autor?.Nombre,
                Categoria = l.Categoria?.Nombre,
                AutorId = l.AutorId,
                CategoriaId = l.CategoriaId
            }).ToList();

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CrearLibro([FromBody] CrearLibroDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Titulo))
                return BadRequest("El título del libro es obligatorio.");

            var autor = await _unitOfWork.Autores
                .FindFirstOrDefaultAsync(a => a.Nombre.ToLower() == dto.Autor.ToLower());

            if (autor == null)
            {
                autor = new Autor { Nombre = dto.Autor };
                await _unitOfWork.Autores.AddAsync(autor);
                await _unitOfWork.SaveChangesAsync();
            }

            var categoria = await _unitOfWork.Categorias
                .FindFirstOrDefaultAsync(c => c.Nombre.ToLower() == dto.Categoria.ToLower());

            if (categoria == null)
            {
                categoria = new Categoria { Nombre = dto.Categoria };
                await _unitOfWork.Categorias.AddAsync(categoria);
                await _unitOfWork.SaveChangesAsync();
            }

            var libro = new Libro
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Disponible = dto.Disponible,
                AutorId = autor.Id,
                CategoriaId = categoria.Id
            };

            await _unitOfWork.Libros.AddAsync(libro);
            await _unitOfWork.SaveChangesAsync();

            return Ok(libro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarLibro(int id)
        {
            var libro = await _unitOfWork.Libros.GetByIdAsync(id);
            if (libro == null)
                return NotFound();

            _unitOfWork.Libros.Delete(libro);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<LibroDTO>>> BuscarLibros(
            string? titulo, string? categoria, decimal? maxPrecio, bool? disponible)
        {
            var resultado = await _libroService.BuscarLibrosAsync(titulo, categoria, maxPrecio, disponible);
            return Ok(resultado);
        }

        [HttpGet("{id}/detalles")]
        public async Task<ActionResult<LibroDTO>> Detalles(int id)
        {
            var libro = await _libroService.ObtenerDetallesAsync(id);
            if (libro == null) return NotFound();
            return Ok(libro);
        }

        [HttpPost("{id}/comprar")]
        public async Task<IActionResult> Comprar(long id, [FromQuery] long usuarioId)
        {
            await _libroService.ComprarLibroAsync(usuarioId, id);
            return Ok(new { mensaje = "Compra realizada con éxito." });
        }

        [HttpPost("{id}/reservar")]
        public async Task<IActionResult> Reservar(long id, [FromQuery] long usuarioId)
        {
            await _libroService.ReservarLibroAsync(usuarioId, id);
            return Ok(new { mensaje = "Reserva realizada con éxito." });
        }

    }
}
