using LojaVirtual.Services.Produto;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoInterface _produtoInterface;
        public ProdutoController(IProdutoInterface produtoInterface)
        {
            _produtoInterface = produtoInterface;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoInterface.ListarProdutos();
            return View(produtos);
        }
    }
}
