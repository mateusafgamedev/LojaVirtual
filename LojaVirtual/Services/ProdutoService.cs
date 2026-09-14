using LojaVirtual.Data;
using LojaVirtual.DTO.ProdutoDto;
using LojaVirtual.Models;
using LojaVirtual.Repository;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Services
{

    public class ProdutoService : IProdutoInterface
    {
        private readonly AppDbContext _context;
        private readonly string _environment;
        public ProdutoService(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment.WebRootPath;
        }

        public async Task<ProdutoModel> CadastrarProduto(CriarProdutoDto produtoDto, IFormFile foto)
        {
            try
            {
                var caminhoImagem = GerarCaminhoImagem(foto);

                var produto = new ProdutoModel
                {
                    Nome = produtoDto.Nome,
                    Marca = produtoDto.Marca,
                    Modelo = produtoDto.Modelo,
                    Foto = caminhoImagem,
                    Valor = produtoDto.Valor.Value,
                    QuantidadeEmEstoque = produtoDto.QuantidadeEmEstoque.Value,
                    CategoriaId = produtoDto.CategoriaId.Value
                };
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }

        public async Task<ProdutoModel> ExcluirProduto(int id)
        {
            try
            {
                var produto = await _context.Produtos.FindAsync(id);
                if (produto == null)
                {
                    throw new Exception("Produto não cadastrado ou não encontrado.");
                }
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ProdutoModel>> ListarProdutos()
        {
            try
            {

                return await _context.Produtos.Include(c => c.Categoria).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar produtos: {ex.Message}");
            }
            
        }


        private string GerarCaminhoImagem(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeArquivoImg = foto.FileName.Replace(" ", "").ToLower() + "_" + codigoUnico + ".png";
            
            var caminhoImagem = _environment + "\\img\\";

            if (!Directory.Exists(caminhoImagem))
            {
                Directory.CreateDirectory(caminhoImagem);
            }

            using (var stream = File.Create(caminhoImagem + nomeArquivoImg))
            {
                foto.CopyToAsync(stream).Wait();
            }
            return nomeArquivoImg;
        }
    }
}
