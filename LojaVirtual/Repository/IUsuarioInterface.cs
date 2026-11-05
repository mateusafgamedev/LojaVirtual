using LojaVirtual.DTO.LoginDto;
using LojaVirtual.DTO.UsuarioDto;
using LojaVirtual.Models;

namespace LojaVirtual.Repository
{
    public interface IUsuarioInterface
    {
        
        Task<UsuarioModel> BuscarUsuarioPorId(int id);
        Task<List<UsuarioModel>> ListarUsuarios();
        Task<bool> VerificarUsuarioExistente(CadastrarUsuarioDto usuarioDto);
        Task<CadastrarUsuarioDto> CadastrarUsuario(CadastrarUsuarioDto usuarioDto);
        Task<UsuarioModel> AtualizarUsuario(EditarUsuarioDto usuarioDto);
        Task<UsuarioModel> Login(LoginUsuarioDto loginDto);


    }
}
