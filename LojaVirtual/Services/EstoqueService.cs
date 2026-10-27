using LojaVirtual.Data;
using LojaVirtual.Models;
using LojaVirtual.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

        public List<RegistroProdutosModel> ListarRegistrosDeVendas()
        {
            try
            {
                var result = from pb in _context.ProdutosBaixados.Include(x => x.Produto)
                                                                        .Include(y => y.Produto.Categoria)
                                                                        .ToList()
                             group pb by new { pb.Produto.CategoriaId, pb.DataDaBaixa } into total
                             select new
                             {
                                 ProdutoId = total.First().Produto.Categoria.Id,
                                 CategoriaNome = total.First().Produto.Categoria.Nome,
                                 DataDaCompra = total.First().DataDaBaixa,
                                 Total = total.Sum(x => x.Produto.Valor)
                             };

                var totalGeral = _context.ProdutosBaixados.Include(x => x.Produto)
                                                            .Include(y => y.Produto.Categoria)
                                                            .Sum(x => x.Produto.Valor);

                List<RegistroProdutosModel> ListaRegistro = new List<RegistroProdutosModel>();

                foreach (var resulList in result)
                {
                    var registro = new RegistroProdutosModel
                    {
                        ProdutoId = resulList.ProdutoId,
                        NomeCategoria = resulList.CategoriaNome,
                        ValorTotal = resulList.Total,
                        DataCompra = resulList.DataDaCompra,
                        ValorTotalGeral = totalGeral
                    };
                    ListaRegistro.Add(registro);
                }

                return ListaRegistro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
