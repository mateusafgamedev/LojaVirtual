using LojaVirtual.DTO.ProdutoDto;
using LojaVirtual.Models;

namespace LojaVirtual.Repository
{
    public interface IProdutoInterface
    {
        Task<List<ProdutoModel>> ListarProdutos();
        Task<ProdutoModel> CadastrarProduto(CriarProdutoDto produtoDto, IFormFile foto);
        Task<ProdutoModel> BuscarProdutoPorId(int id);
        Task<ProdutoModel> AtualizarProduto(int id, EditarProdutoDto atualizarProdutoDto, IFormFile foto);
        Task<ProdutoModel> ExcluirProduto(int id);
    }
}
