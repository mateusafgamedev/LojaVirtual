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
                var totalGeral = _context.ProdutosBaixados.Sum(p => p.ValorProduto);

                return _context.ProdutosBaixados
                    .GroupBy(p => new { p.NomeCategoria, p.DataDaBaixa })
                    .Select(g => new RegistroProdutosModel
                    {
                        NomeCategoria = g.Key.NomeCategoria,
                        DataCompra = g.Key.DataDaBaixa,
                        ValorTotal = g.Sum(p => p.ValorProduto),
                        ValorTotalGeral = totalGeral
                    })
                    .ToList();

                //var result = from pb in _context.ProdutosBaixados.Include(x => x.Produto)
                //                                                        .Include(y => y.Produto.Categoria)
                //                                                        .ToList()
                //             group pb by new { pb.Produto.CategoriaId, pb.DataDaBaixa } into total
                //             select new
                //             {
                //                 ProdutoId = total.First().Produto.Categoria.Id,
                //                 CategoriaNome = total.First().Produto.Categoria.Nome,
                //                 DataDaCompra = total.First().DataDaBaixa,
                //                 Total = total.Sum(x => x.Produto.Valor)
                //             };

                //var totalGeral = _context.ProdutosBaixados.Include(x => x.Produto)
                //                                            .Include(y => y.Produto.Categoria)
                //                                            .Sum(x => x.Produto.Valor);

                //List<RegistroProdutosModel> ListaRegistro = new List<RegistroProdutosModel>();

                //foreach (var resulList in result)
                //{
                //    var registro = new RegistroProdutosModel
                //    {
                //        ProdutoId = resulList.ProdutoId,
                //        NomeCategoria = resulList.CategoriaNome,
                //        ValorTotal = resulList.Total,
                //        DataCompra = resulList.DataDaCompra,
                //        ValorTotalGeral = totalGeral
                //    };
                //    ListaRegistro.Add(registro);
                //}

                //return ListaRegistro;
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
                    ProdutoModelId = idProduto,
                    NomeProduto = produto.Nome,
                    NomeCategoria = produto.Categoria.Nome,
                    ValorProduto = produto.Valor,
                };

                _context.ProdutosBaixados.Add(produtoBaixado);
                BaixarEstoque(produto);
                await _context.SaveChangesAsync();

                //_context.Update(produto);
                //await _context.SaveChangesAsync();

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
