using Business.DTOs;
using Business.Interfaces.Usuarios;
using Business.Services.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Presentation.Controllers.Usuarios
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            try
            {
                var loginResult = _authService.Login(dto);
                return Ok(loginResult);
            }
            catch (Exception ex)
            {
                return StatusCode(401, new { mensaje = ex.Message });
            }
        }



        [HttpPost("register")]
        public IActionResult Registrar([FromBody] RegistroDTO dto)
        {
            try
            {
                _authService.Registrar(dto);
                return Ok(new { mensaje = "Usuario registrado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [HttpPost("test-password")]
        public IActionResult TestPassword([FromBody] TestPasswordDTO dto)
        {
            try
            {

                bool ok = _authService.VerificarHash(dto);
                return Ok(new { valido = ok });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("usuario")]
        public IActionResult ObtenerUsuarioActual()
        {
            try
            {
                var claims = User.Claims.ToList();
                var idClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
                var correoClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);
                var nombreClaim = claims.FirstOrDefault(c => c.Type == "nombre");

                if (idClaim == null || correoClaim == null || nombreClaim == null)
                    return StatusCode(401, new { mensaje = "Token inválido: faltan claims necesarios" });

                var id = int.Parse(idClaim.Value);
                var usuarioDb = _authService.ObtenerUsuarioPorId(id);

                var usuarioDto = new UsuarioDTO
                {
                    Id = usuarioDb.Id,
                    NombreCompleto = usuarioDb.Nombre,
                    Correo = usuarioDb.Correo,
                    Saldo = usuarioDb.Saldo,
                    AvatarUrl = usuarioDb.AvatarUrl,
                    Direccion = usuarioDb.Direccion
                };

                return Ok(usuarioDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }





    }

}
