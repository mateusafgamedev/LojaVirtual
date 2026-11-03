using LojaVirtual.DTO.UsuarioDto;
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
        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario(CadastrarUsuarioDto usuarioDto)
        {
            

            if (ModelState.IsValid)
            {
                if( await _usuarioInterface.VerificarUsuarioExistente(usuarioDto))
                {
                    TempData["MenssagemErro"] = "O usuário já está cadastrado.";
                    return View(usuarioDto);
                }
                var usuarioCadastrado = await _usuarioInterface.CadastrarUsuario(usuarioDto);
                TempData["MenssagemSucesso"] = "Usuário cadastrado com sucesso.";
                return RedirectToAction("Index");

            } else
            {
                TempData["MenssagemErro"] = "Ocorreu um erro ao cadastrar o usuário. Verifique os campos e tente novamente.";
                return View(usuarioDto);
            }
        }
    }
}
