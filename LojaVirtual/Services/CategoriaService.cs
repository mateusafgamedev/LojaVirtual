using LojaVirtual.Data;
using LojaVirtual.Models;
using LojaVirtual.Repository;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Services
{
    public class CategoriaService : ICategoriaInterface
    {
        private readonly AppDbContext _context;
        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoriaModel>> BuscarCategoriasParaProdutos()
        {
            try
            {
                return await _context.Categorias.ToListAsync();
            }
            catch (Exception ex) 
            { 
              throw new Exception("Erro ao buscar categorias.", ex);
            }
        }
    }
}
