using LojaVirtual.DTO.ProdutoDto;
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
            ViewBag.Categorias = await _categoriaInterface.BuscarCategoriasParaProdutos();

            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarProduto(CriarProdutoDto produtoDto, IFormFile foto)
        {
            if(ModelState.IsValid)
            {
                var produto = await _produtoInterface.CadastrarProduto(produtoDto, foto);
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                ViewBag.Categorias = await _categoriaInterface.BuscarCategoriasParaProdutos();
                return View(produtoDto);
            }

            
        }

        public async Task<IActionResult> ExcluirProduto(int id)
        {
            await _produtoInterface.ExcluirProduto(id);
            return RedirectToAction("Index", "Produto");
        }

    }
}
