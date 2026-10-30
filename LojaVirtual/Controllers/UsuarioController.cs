using LojaVirtual.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioInterface.ListarUsuarios();
            return View(usuarios);
        }

        public IActionResult CadastrarUsuario()
        {
            return View();
        }
    }
}
