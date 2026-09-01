using LojaVirtual.Models;

namespace LojaVirtual.Services.Produto
{
    public interface IProdutoInterface
    {

        Task<List<ProdutoModel>> ListarProdutos();
    }
}
