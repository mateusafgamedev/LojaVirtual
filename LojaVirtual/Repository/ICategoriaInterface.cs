using LojaVirtual.Models;

namespace LojaVirtual.Repository
{
    public interface ICategoriaInterface
    {
        Task<List<CategoriaModel>> BuscarCategoria();
    }
}
