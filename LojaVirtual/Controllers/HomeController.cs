
using LojaVirtual.Models;
using LojaVirtual.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProdutoInterface _produtoInterface;
        public HomeController(IProdutoInterface produtoInterface)
        {
            _produtoInterface = produtoInterface;
        }

        public async Task<IActionResult> Index(string? pesquisar)
        {
            List<ProdutoModel> produtos = new List<ProdutoModel>();

            if (pesquisar == null)
            {
                produtos = await _produtoInterface.ListarProdutos();
            }
            else
            {
                produtos = await _produtoInterface.BuscarProdutoPorFiltro(pesquisar);
            }
            return View(produtos);
        }

       
    }
}
