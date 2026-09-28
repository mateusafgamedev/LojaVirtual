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
                TempData["MenssagemSucesso"] = "Produto cadastrado com sucesso!";
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                ViewBag.Categorias = await _categoriaInterface.BuscarCategoriasParaProdutos();
                TempData["MenssagemErro"] = "Erro ao cadastrar produto!";
                return View(produtoDto);
            }
        }

        public async Task<IActionResult> EditarProduto(int id)
        {
            ViewBag.Categorias = await _categoriaInterface.BuscarCategoriasParaProdutos();

            var produto = await _produtoInterface.BuscarProdutoPorId(id);

            var editarProdutoDto = new EditarProdutoDto
            {
                Nome = produto.Nome,
                Marca = produto.Marca,
                Modelo = produto.Modelo,
                Foto = produto.Foto,
                Valor = produto.Valor,
                QuantidadeEmEstoque = produto.QuantidadeEmEstoque,
                CategoriaId = produto.CategoriaId
            };
            return View(editarProdutoDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditarProduto(int id, EditarProdutoDto produtoDto, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                var produto = await _produtoInterface.AtualizarProduto(id, produtoDto, foto);
                TempData["MenssagemSucesso"] = "Dados editados com sucesso!";
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                TempData["MenssagemErro"] = "Erro ao editar dados do produto!";
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
