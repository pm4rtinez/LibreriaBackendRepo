using Business.DTOs;
using Business.Services.Comprobantes;
using Data.Access.Entities.Comprobantes;
using Data.Access.Entities.Reservas;
using Data.Access.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Comprobantes
{
    [Route("api/[controller]")]
    public class ComprobanteDevolucionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ComprobanteService _comprobanteService;

        public ComprobanteDevolucionController(IUnitOfWork unitOfWork, ComprobanteService comprobanteService)
        {
            _unitOfWork = unitOfWork;
            _comprobanteService = comprobanteService;
        }

        [HttpPost("devolver-con-firma/{reservaId}")]
        public async Task<IActionResult> DevolverConFirma(long reservaId, [FromBody] DevolucionConFirmaDTO dto)
        {
            var reserva = await _unitOfWork.Reserva.GetByIdWithLibroAsync(reservaId);
            if (reserva == null) return NotFound("Reserva no encontrada.");

            if (reserva.EstadoReserva != EstadoReservaId.Activo)
                return BadRequest("La reserva no está activa.");

            var comprobante = new ComprobanteDevolucion
            {
                ReservaId = reservaId,
                FechaGeneracion = DateTime.UtcNow
            };

            await _unitOfWork.ComprobanteDevolucion.AddAsync(comprobante);
            await _unitOfWork.SaveChangesAsync(); // Guardamos primero para tener ID

            var resultado = await _comprobanteService.GuardarDniYFirmaAsync(comprobante.Id, dto.DNI, dto.FirmaBase64, _unitOfWork);
            if (!resultado) return BadRequest("No se pudo guardar DNI y firma.");

            reserva.EstadoReserva = EstadoReservaId.Terminado;
            reserva.TieneComprobante = true; // ✅ ESTA ES LA LÍNEA CLAVE
            _unitOfWork.Reserva.Update(reserva);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { mensaje = "Libro devuelto y comprobante guardado." });
        }



        [HttpGet("pdf/{reservaId}")]
        public async Task<IActionResult> DescargarPdf(long reservaId)
        {
            var comprobante = await _unitOfWork.ComprobanteDevolucion
                .GetByReservaIdWithLibroAsync(reservaId);

            if (comprobante == null)
                return NotFound("Comprobante no encontrado.");

            var pdfBytes = _comprobanteService.GenerarPdfDevolucion(comprobante);
            return File(pdfBytes, "application/pdf", $"comprobante_{reservaId}.pdf");
        }


        [HttpGet("detalle/{reservaId}")]
        public async Task<IActionResult> ObtenerDetalle(long reservaId)
        {
            var comprobante = await _unitOfWork.ComprobanteDevolucion.GetByReservaIdWithLibroAsync(reservaId);
            if (comprobante == null)
                return NotFound();

            var diasDePrestamo = (comprobante.FechaGeneracion - comprobante.Reserva.FechaReserva).Days;

            return Ok(new
            {
                dni = comprobante.DNI,
                firmaBase64 = Convert.ToBase64String(comprobante.FirmaImagen),
                fechaDevolucion = comprobante.FechaGeneracion.ToString("yyyy-MM-dd HH:mm"),
                diasDePrestamo = diasDePrestamo
            });
        }



    }
}
