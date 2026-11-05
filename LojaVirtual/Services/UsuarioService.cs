using AutoMapper;
using LojaVirtual.Data;
using LojaVirtual.DTO.LoginDto;
using LojaVirtual.DTO.UsuarioDto;
using LojaVirtual.Models;
using LojaVirtual.Repository;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly AppDbContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;
        private readonly IMapper _mapper;
        private readonly ISessaoInterface _sessaoInterface;

        public UsuarioService(AppDbContext context, IAutenticacaoInterface autenticacaoInterface, IMapper mapper, ISessaoInterface sessaoInterface)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
            _mapper = mapper;
            _sessaoInterface = sessaoInterface;
        }

        public async Task<UsuarioModel> AtualizarUsuario(EditarUsuarioDto atualizarUsuarioDto)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.Include(e => e.Endereco).FirstOrDefaultAsync(u => u.Id == atualizarUsuarioDto.Id);

                if (usuarioExistente == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                usuarioExistente.Nome = atualizarUsuarioDto.Nome;
                usuarioExistente.Email = atualizarUsuarioDto.Email;
                usuarioExistente.Cargo = atualizarUsuarioDto.Cargo;
                usuarioExistente.DataAlteracao = DateTime.Now;
                usuarioExistente.Endereco = _mapper.Map<EnderecoModel>(atualizarUsuarioDto.Endereco);

                _context.Usuarios.Update(usuarioExistente);
                await _context.SaveChangesAsync();

                return usuarioExistente;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> BuscarUsuarioPorId(int id)
        {
            try
            {
                var usuarioCadastrado = await _context.Usuarios
                    .Include(e => e.Endereco)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (usuarioCadastrado == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }   

                return usuarioCadastrado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CadastrarUsuarioDto> CadastrarUsuario(CadastrarUsuarioDto usuarioDto)
        {
            try
            {
                _autenticacaoInterface.CriarSenhaHash(usuarioDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel
                {
                    Nome = usuarioDto.Nome,
                    Email = usuarioDto.Email,
                    Cargo = usuarioDto.Cargo,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                    
                };

                var endereco = new EnderecoModel
                {
                    Logradouro = usuarioDto.Logradouro,
                    Numero = usuarioDto.Numero,
                    Bairro = usuarioDto.Bairro,
                    Complemento = usuarioDto.Complemento,
                    Cidade = usuarioDto.Cidade,
                    Estado = usuarioDto.Estado,
                    CEP = usuarioDto.CEP,
                    Usuario = usuario
                };

                usuario.Endereco = endereco;

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return usuarioDto;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);    
            }
        }

        public async Task<List<UsuarioModel>> ListarUsuarios()
        {
            try
            {
                return await _context.Usuarios.Include(e => e.Endereco).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioModel> Login(LoginUsuarioDto loginDto)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                if (usuario == null)
                {
                    return null;
                }

                if (!_autenticacaoInterface.VerificarSenha(loginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    return null;
                }

                _sessaoInterface.CriarSessao(usuario);

                return usuario; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VerificarUsuarioExistente(CadastrarUsuarioDto usuarioDto)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioDto.Email);

                if (usuarioExistente == null)
                {
                    return false; 
                }

                return true; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
