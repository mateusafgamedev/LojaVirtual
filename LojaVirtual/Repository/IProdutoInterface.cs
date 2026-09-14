using LojaVirtual.DTO.ProdutoDto;
using LojaVirtual.Models;

namespace LojaVirtual.Repository
{
    public interface IProdutoInterface
    {
        Task<List<ProdutoModel>> ListarProdutos();
        Task<ProdutoModel> CadastrarProduto(CriarProdutoDto produtoDto, IFormFile foto);
    }
}
