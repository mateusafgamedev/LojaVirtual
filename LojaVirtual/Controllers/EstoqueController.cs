using LojaVirtual.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly IEstoqueInterface _estoqueInterface;

        public EstoqueController(IEstoqueInterface estoqueInterface)
        {
            _estoqueInterface = estoqueInterface;
        }
        public IActionResult Index()
        {
            var registros = _estoqueInterface.ListarRegistrosDeVendas();
            return View(registros);
        }

        [HttpPost]
        public async Task<IActionResult> BaixarEstoque(int id)
        {
            await _estoqueInterface.CriarRegistro(id);
            TempData["MenssagemSucesso"] = "Compra realizada com sucesso!";
            return RedirectToAction("Index", "Home");
        }
    }
}
