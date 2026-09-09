using LojaVirtual.Data;
using LojaVirtual.Models;
using LojaVirtual.Repository;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Services
{

    public class ProdutoService : IProdutoInterface
    {
        private readonly AppDbContext _context;
        public ProdutoService(AppDbContext context)
        {
            _context = context;
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
    }
}
