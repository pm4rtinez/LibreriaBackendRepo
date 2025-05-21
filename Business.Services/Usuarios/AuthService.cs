using Business.DTOs;
using Business.Interfaces.Usuarios;
using Data.Access.EF.Context;
using Data.Access.Entities.Usuarios;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Business.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Business.Services.Usuarios
{
    public class AuthService : IAuthService
    {
        private readonly LibreriaDbContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        private readonly JwtSettings _jwtSettings;

        public AuthService(
            LibreriaDbContext context,
            IPasswordHasher<Usuario> passwordHasher,
            IOptions<JwtSettings> jwtOptions)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtSettings = jwtOptions.Value;
        }


        public string Login(LoginDTO dto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == dto.Correo);
            if (usuario == null) throw new Exception("Usuario no encontrado");

            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Contraseña, dto.Contraseña);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Contraseña incorrecta");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Correo),
                new Claim("nombre", usuario.Nombre)
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void Registrar(RegistroDTO dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                throw new Exception("Las contraseñas no coinciden");

            if (_context.Usuarios.Any(u => u.Correo == dto.Correo))
                throw new Exception("Correo ya registrado");

            var usuario = new Usuario
            {
                Nombre = dto.NombreCompleto,
                Correo = dto.Correo,
                Contraseña = _passwordHasher.HashPassword(null, dto.Password),
                Saldo = 0
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }



        public void RecuperarContrasena(string correo)
        {
            //Implementare si hace falta algun dia lo del correo de recuperacion pero de momento no esta
        }

        public void ConfirmarCodigo(string correo, string codigo)
        {
            // Aqui va la confirmacion del codigo que se recibe
        }

        public Task<string> LoginAsync(LoginDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task RegistrarAsync(RegistroDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task RecuperarContrasenaAsync(string correo)
        {
            throw new NotImplementedException();
        }

        public Task ConfirmarCodigoRecuperacionAsync(string correo, string codigo)
        {
            throw new NotImplementedException();
        }
    }

}
