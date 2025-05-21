using Business.DTOs;
using Business.Interfaces.Reservas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers.Reservas
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        // GET: api/reservas/usuario/id
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<List<ReservaDTO>>> ObtenerPorUsuario(int usuarioId)
        {
            var reservas = await _reservaService.ObtenerReservasPorUsuarioAsync(usuarioId);
            return Ok(reservas);
        }

        // GET: api/reservas/usuario/entre
        [HttpGet("usuario/{usuarioId}/entre")]
        public async Task<ActionResult<List<ReservaDTO>>> ObtenerPorFechas(int usuarioId, DateTime desde, DateTime hasta)
        {
            var reservas = await _reservaService.ObtenerReservasPorFechaAsync(usuarioId, desde, hasta);
            return Ok(reservas);
        }

        // POST: api/reservas/registrar
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(int usuarioId, int libroId)
        {
            await _reservaService.RegistrarReservaAsync(usuarioId, libroId);
            return Ok(new { mensaje = "Reserva registrada correctamente." });
        }

        // PUT: api/reservas/devolver
        [HttpPut("devolver/{reservaId}")]
        public async Task<IActionResult> Devolver(int reservaId)
        {
            await _reservaService.DevolverLibroAsync(reservaId);
            return Ok(new { mensaje = "Libro devuelto correctamente." });
        }

        // GET: api/reservas/{reservaId}/dias-restantes
        [HttpGet("{reservaId}/dias-restantes")]
        public async Task<ActionResult<int>> DiasRestantes(int reservaId)
        {
            var dias = await _reservaService.DiasRestantesAsync(reservaId);
            return Ok(new { diasRestantes = dias });
        }
    }
}
