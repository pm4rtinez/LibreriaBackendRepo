using Data.Access.Entities.Categorias;
using Data.Access.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers.Categorias
{
    public class CategoriasController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            var categorias = await _unitOfWork.Categorias.GetAllAsync();
            return Ok(categorias);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> CrearCategoria([FromBody] Categoria categoria)
        {
            await _unitOfWork.Categorias.AddAsync(categoria);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategorias), new { id = categoria.Id }, categoria);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var categoria = await _unitOfWork.Categorias.GetByIdAsync(id);
            if (categoria == null) return NotFound();

            _unitOfWork.Categorias.Delete(categoria);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
