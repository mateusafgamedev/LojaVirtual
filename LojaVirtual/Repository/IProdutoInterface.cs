using LojaVirtual.Models;

namespace LojaVirtual.Repository
{
    public interface IProdutoInterface
    {

        Task<List<ProdutoModel>> ListarProdutos();
    }
}
