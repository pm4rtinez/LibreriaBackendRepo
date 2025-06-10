using Business.DTOs;
using Business.DTOs.Business.DTOs;
using Business.Interfaces.Usuarios;
using Data.Access.Entities.Usuarios;
using Data.Access.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers.Usuarios
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUnitOfWork unitOfWork, IUsuarioService usuarioService)
        {
            _unitOfWork = unitOfWork;
            _usuarioService = usuarioService;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _unitOfWork.Usuarios.GetAllAsync();
            return Ok(usuarios);
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> CrearUsuario([FromBody] CrearUsuarioDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre) || string.IsNullOrWhiteSpace(dto.Correo))
                return BadRequest("Datos inválidos");

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Password = dto.Password,
                Saldo = dto.Saldo,
                AvatarUrl = dto.AvatarUrl,
                Direccion = dto.Direccion
            };

            await _unitOfWork.Usuarios.AddAsync(usuario);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.Id }, usuario);
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _unitOfWork.Usuarios.GetByIdAsync(id);
            if (usuario == null) return NotFound();

            _unitOfWork.Usuarios.Delete(usuario);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/usuarios/{id}/perfil
        [HttpGet("{id}/perfil")]
        public ActionResult<UsuarioDTO> ObtenerPerfil(long id)
        {
            var perfil = _usuarioService.ObtenerPerfil(id);
            if (perfil == null) return NotFound();
            return Ok(perfil);
        }

        // POST: api/usuarios/{id}/saldo
        [HttpPost("{id}/saldo")]
        public IActionResult AñadirSaldo(long id, [FromBody] decimal cantidad)
        {
            if (cantidad <= 0) return BadRequest("Cantidad inválida");
            _usuarioService.AñadirSaldo(id, cantidad);
            return NoContent();
        }

        // GET: api/usuarios/{id}/reservas-activas
        [HttpGet("{id}/reservas-activas")]
        public ActionResult<IEnumerable<ReservaDTO>> ObtenerReservasActivas(long id)
        {
            var reservas = _usuarioService.ObtenerReservasActivas(id);
            return Ok(reservas);
        }

        // GET: api/usuarios/{id}/historial
        [HttpGet("{id}/historial")]
        public ActionResult<HistorialDTO> ObtenerHistorialCompleto(long id)
        {
            var historial = _usuarioService.ObtenerHistorialCompleto(id);
            return Ok(historial);
        }



        [HttpPut("{id}/avatar")]
        public async Task<IActionResult> ActualizarAvatar(long id, [FromBody] string nuevaUrl)
        {
            var usuario = await _unitOfWork.Usuarios.GetByIdAsync(id);
            if (usuario == null) return NotFound("Usuario no encontrado");

            usuario.AvatarUrl = nuevaUrl;
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { mensaje = "Avatar actualizado correctamente" });
        }

    }
}
