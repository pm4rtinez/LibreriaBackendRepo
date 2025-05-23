using Business.DTOs;
using Business.Interfaces.Usuarios;
using Microsoft.AspNetCore.Mvc;

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
            Console.WriteLine("[BACK] /api/auth/login llamado con:");
            Console.WriteLine($"Correo: {dto.Correo}, Password: {dto.Password}");

            try
            {
                var token = _authService.Login(dto);
                Console.WriteLine("[BACK] ✅ Login correcto, token generado.");
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                Console.WriteLine("[BACK] ❌ Login fallido:", ex.Message);
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
    }

}
