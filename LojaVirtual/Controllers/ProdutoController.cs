using LojaVirtual.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoInterface _produtoInterface;
        private readonly ICategoriaInterface _categoriaInterface;
        public ProdutoController(IProdutoInterface produtoInterface, 
                                    ICategoriaInterface categoriaInterface)
        {
            _produtoInterface = produtoInterface;
            _categoriaInterface = categoriaInterface;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoInterface.ListarProdutos();
            return View(produtos);
        }

        public async Task<IActionResult> CadastrarProduto() 
        {
            ViewBag.Categoria = await _categoriaInterface.BuscarCategoria();

            return View(); 
        }

    }
}
