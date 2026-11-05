using LojaVirtual.DTO.LoginDto;
using LojaVirtual.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioInterface _usuarioInterface;

        public LoginController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUsuarioDto login)
        {

            if (ModelState.IsValid)
            {
                var usuario = await _usuarioInterface.Login(login);

                if (usuario == null)
                {
                    TempData["MensagemErro"] = "Usuário ou senha inválidos.";
                    return View(login);
                }

                TempData["MensagemSucesso"] = "Login realizado com sucesso.";
                return RedirectToAction("Index", "Home");

            } else
            {
                TempData["MensagemErro"] = "Usuário ou senha inválidos.";
                return View(login);
            }
        }
    }
}
