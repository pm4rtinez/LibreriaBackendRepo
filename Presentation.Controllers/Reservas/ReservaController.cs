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
        public async Task<ActionResult<List<ReservaDTO>>> ObtenerPorUsuario(long usuarioId)
        {


            var reservas = await _reservaService.ObtenerReservasPorUsuarioAsync(usuarioId);
            return Ok(reservas);
        }

        // GET: api/reservas/usuario/entre
        [HttpGet("usuario/{usuarioId}/entre")]
        public async Task<ActionResult<List<ReservaDTO>>> ObtenerPorFechas(long usuarioId, DateTime desde, DateTime hasta)
        {
            var reservas = await _reservaService.ObtenerReservasPorFechaAsync(usuarioId, desde, hasta);
            return Ok(reservas);
        }

        // POST: api/reservas/registrar
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] ReservaRequestDTO dto)
        {
            await _reservaService.RegistrarReservaAsync(dto.UsuarioId, dto.LibroId);
            return Ok(new { mensaje = "Reserva registrada correctamente." });
        }


        // PUT: api/reservas/devolver
        [HttpPut("devolver/{reservaId}")]
        public async Task<IActionResult> Devolver(long reservaId)
        {
            await _reservaService.DevolverLibroAsync(reservaId);
            return Ok(new { mensaje = "Libro devuelto correctamente." });
        }

        // GET: api/reservas/{reservaId}/dias-restantes
        [HttpGet("{reservaId}/dias-restantes")]
        public async Task<ActionResult<long>> DiasRestantes(long reservaId)
        {
            var dias = await _reservaService.DiasRestantesAsync(reservaId);
            return Ok(new { diasRestantes = dias });
        }

        // PUT: api/reservas/ampliar/{reservaId}?dias=5
        [HttpPut("ampliar/{reservaId}")]
        public async Task<IActionResult> Ampliar(long reservaId, [FromQuery] int dias)
        {
            if (dias < 1 || dias > 7)
                return BadRequest(new { mensaje = "Solo puedes ampliar entre 1 y 7 días." });

            var resultado = await _reservaService.AmpliarReservaAsync(reservaId, dias);

            if (resultado == null)
                return BadRequest(new { mensaje = "No se pudo ampliar la reserva (puede no estar activa o no existir)." });

            return Ok(resultado);
        }






    }
}
