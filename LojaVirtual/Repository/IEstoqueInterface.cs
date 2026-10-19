using LojaVirtual.Models;

namespace LojaVirtual.Repository
{
    public interface IEstoqueInterface
    {
        Task<ProdutosBaixadosModel> CriarRegistro(int id);
    }
}
