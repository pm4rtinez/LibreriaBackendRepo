using Data.Access.Entities.DetallesCompra;
using Data.Access.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers.DetallesCompras
{
    public class DetallesCompraController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetallesCompraController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCompra>>> GetDetalles()
        {
            var detalles = await _unitOfWork.DetalleCompra.GetAllAsync();
            return Ok(detalles);
        }

        [HttpPost]
        public async Task<ActionResult<DetalleCompra>> CrearDetalle([FromBody] DetalleCompra detalle)
        {
            await _unitOfWork.DetalleCompra.AddAsync(detalle);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDetalles), new { id = detalle.Id }, detalle);
        }
    }
}
