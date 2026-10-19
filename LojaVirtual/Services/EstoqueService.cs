using LojaVirtual.Data;
using LojaVirtual.Models;
using LojaVirtual.Repository;

namespace LojaVirtual.Services
{
    public class EstoqueService : IEstoqueInterface
    {
        private readonly AppDbContext _context;
        private readonly IProdutoInterface _produtoInterface;

        public EstoqueService(AppDbContext context, IProdutoInterface produtoInterface)
        {
            _context = context;
            _produtoInterface = produtoInterface;
        }

        public async Task<ProdutosBaixadosModel> CriarRegistro(int idProduto)
        {
            try
            {
                var produto = await _produtoInterface.BuscarProdutoPorId(idProduto);

                var produtoBaixado = new ProdutosBaixadosModel
                {
                    Produto = produto,
                    ProdutoModelId = idProduto
                };
                _context.ProdutosBaixados.Add(produtoBaixado);
                await _context.SaveChangesAsync();

                BaixarEstoque(produto);

                _context.Update(produto);
                await _context.SaveChangesAsync();

                return produtoBaixado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BaixarEstoque(ProdutoModel produto)
        {
            produto.QuantidadeEmEstoque--;
        }
    }
}
