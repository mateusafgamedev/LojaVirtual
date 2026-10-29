using LojaVirtual.Models;

namespace LojaVirtual.Repository
{
    public interface IUsuarioInterface
    {
        
        Task<UsuarioModel> BuscarUsuarioPorId(int id);
        Task<List<UsuarioModel>> ListarUsuarios();


    }
}
