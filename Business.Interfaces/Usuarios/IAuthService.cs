using Business.DTOs;
using Data.Access.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Usuarios
{
    public interface IAuthService
    {
        void Registrar(RegistroDTO dto);    
        Task<string> LoginAsync(LoginDTO dto);
        Task RegistrarAsync(RegistroDTO dto);
        Task RecuperarContrasenaAsync(string correo);
        Task ConfirmarCodigoRecuperacionAsync(string correo, string codigo);
        bool VerificarHash(TestPasswordDTO dto);
        LoginResultDTO Login(LoginDTO dto);
        Usuario ObtenerUsuarioPorId(int id);

    }

}
