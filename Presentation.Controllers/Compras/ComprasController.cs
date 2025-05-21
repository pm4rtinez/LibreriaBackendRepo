using Business.DTOs;
using Business.Interfaces.Compras;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers.Compras
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraService _compraService;

        public ComprasController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        // GET: api/compras/usuario
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<List<CompraDTO>>> ObtenerComprasPorUsuario(long usuarioId)
        {
            var compras = await _compraService.ObtenerComprasPorUsuario(usuarioId);
            return Ok(compras);
        }

        // GET
        [HttpGet("usuario/{usuarioId}/entre")]
        public async Task<ActionResult<List<CompraDTO>>> ObtenerComprasPorFecha(long usuarioId, DateTime desde, DateTime hasta)
        {
            var compras = await _compraService.ObtenerComprasPorFecha(usuarioId, desde, hasta);
            return Ok(compras);
        }

        // POST: api/compras/registrar
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarCompra([FromQuery] long usuarioId, [FromQuery] long libroId)
        {
            await _compraService.RegistrarCompra(usuarioId, libroId);
            return Ok(new { mensaje = "Compra registrada correctamente." });
        }
    }
}
