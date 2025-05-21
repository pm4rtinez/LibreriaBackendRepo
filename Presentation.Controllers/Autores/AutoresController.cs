using Business.DTOs;
using Data.Access.Entities.Autores;
using Data.Access.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers.Autores
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AutoresController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/autores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> ObtenerAutores()
        {
            var autores = await _unitOfWork.Autores.GetAllAsync();
            return Ok(autores);
        }

        // GET: api/autores/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> GetAutor(long id)
        {
            var autor = await _unitOfWork.Autores.GetByIdAsync(id);
            if (autor == null) return NotFound();
            return Ok(autor);
        }

        // POST: api/autores
        [HttpPost]
        public IActionResult CrearAutor([FromBody] AutorDTO dto)
        {
            var autor = new Autor
            {
                Nombre = dto.Nombre
            };

            _unitOfWork.Autores.AddAsync(autor);
            _unitOfWork.SaveChangesAsync();

            return Ok(autor);
        }


        // PUT: api/autores/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarAutor(int id, [FromBody] Autor autor)
        {
            if (id != autor.Id) return BadRequest("ID no coincide");

            _unitOfWork.Autores.Update(autor);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/autores/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarAutor(int id)
        {
            var autor = await _unitOfWork.Autores.GetByIdAsync(id);
            if (autor == null) return NotFound();

            _unitOfWork.Autores.Delete(autor);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
