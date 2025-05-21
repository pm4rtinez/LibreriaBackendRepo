using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Usuarios
{
    public interface IAuthService
    {
        string Login(LoginDTO dto);
        void Registrar(RegistroDTO dto);    
        Task<string> LoginAsync(LoginDTO dto);
        Task RegistrarAsync(RegistroDTO dto);
        Task RecuperarContrasenaAsync(string correo);
        Task ConfirmarCodigoRecuperacionAsync(string correo, string codigo);
    }

}
