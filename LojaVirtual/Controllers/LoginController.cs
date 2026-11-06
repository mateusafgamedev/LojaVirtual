using LojaVirtual.DTO.LoginDto;
using LojaVirtual.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioInterface _usuarioInterface;
        private readonly ISessaoInterface _sessaoInterface;

        public LoginController(IUsuarioInterface usuarioInterface, ISessaoInterface sessaoInterface)
        {
            _usuarioInterface = usuarioInterface;
            _sessaoInterface = sessaoInterface;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Sair()
        {
            _sessaoInterface.RemoverSessao();
            return RedirectToAction("Login","Login");
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
