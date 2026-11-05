using AutoMapper;
using LojaVirtual.DTO.EnderecoDto;
using LojaVirtual.DTO.UsuarioDto;
using LojaVirtual.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioInterface usuarioInterface, IMapper mapper)
        {
            _usuarioInterface = usuarioInterface;
            _mapper = mapper;
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
                if (await _usuarioInterface.VerificarUsuarioExistente(usuarioDto))
                {
                    TempData["MensagemErro"] = "O usuário já está cadastrado.";
                    return View(usuarioDto);
                }
                var usuarioCadastrado = await _usuarioInterface.CadastrarUsuario(usuarioDto);
                TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso.";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao cadastrar o usuário. Verifique os campos e tente novamente.";
                return View(usuarioDto);
            }
        }

        public async Task<IActionResult> EditarUsuario(int id)
        {
            var usuario = await _usuarioInterface.BuscarUsuarioPorId(id);

            var usuarioEditado = new EditarUsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Cargo = usuario.Cargo,
                Endereco = _mapper.Map<EditarEnderecoDto>(usuario.Endereco)
            };

            return View(usuarioEditado);
        }

        [HttpPost]
        public async Task<IActionResult> EditarUsuario(EditarUsuarioDto usuarioDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioInterface.AtualizarUsuario(usuarioDto);
                TempData["MensagemSucesso"] = "Usuário atualizado com sucesso.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao atualizar o usuário. Verifique os campos e tente novamente.";
                return View(usuarioDto);
            }
        }
    }
}
