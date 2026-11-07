using LojaVirtual.DTO.LoginDto;
using LojaVirtual.DTO.UsuarioDto;
using LojaVirtual.Enum;
using LojaVirtual.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioInterface _usuarioInterface;
        private readonly ISessaoInterface _sessaoInterface;
        private readonly IConfiguration _configuration;

        public LoginController(IUsuarioInterface usuarioInterface, ISessaoInterface sessaoInterface, IConfiguration configuration)
        {
            _usuarioInterface = usuarioInterface;
            _sessaoInterface = sessaoInterface;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult EscolherCadastro()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessaoInterface.RemoverSessao();
            return RedirectToAction("Login","Login");
        }

        public IActionResult CriarConta()
        {
            ViewBag.Admin = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarConta(CadastrarUsuarioDto usuarioDto)
        {
            ViewBag.Admin = false;
            usuarioDto.Cargo = CargoEnum.Cliente;
            return await Cadastrar(usuarioDto);
        }

        public IActionResult CriarContaAdmin()
        {
            ViewBag.Admin = true;
            return View("CriarConta");
        }

        [HttpPost]
        public async Task<IActionResult> CriarContaAdmin(CadastrarUsuarioDto usuarioDto, string adminToken)
        {
            ViewBag.Admin = true;
            var tokenConfigurado = _configuration["AdminToken"];
            if(string.IsNullOrEmpty(tokenConfigurado) || tokenConfigurado != adminToken)
            {
                TempData["MensagemErro"] = "Token de administrador inválido.";
                return View("CriarConta", usuarioDto);
            }
            usuarioDto.Cargo = CargoEnum.Administrador;
            return await Cadastrar(usuarioDto);
        }

        private async Task<IActionResult> Cadastrar(CadastrarUsuarioDto usuarioDto)
        {
            if (ModelState.IsValid)
            {
                if (await _usuarioInterface.VerificarUsuarioExistente(usuarioDto))
                {
                    TempData["MensagemErro"] = "O Email já está cadastrado.";
                    return View("CriarConta", usuarioDto);
                }
                var usuarioCadastrado = await _usuarioInterface.CadastrarUsuario(usuarioDto);
                TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso.";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao cadastrar o usuário. Verifique os campos e tente novamente.";
                return View("CriarConta", usuarioDto);
            }
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
                TempData["MensagemErro"] = "Informe Usuário e senha.";
                return View(login);
            }
        }
    }
}
