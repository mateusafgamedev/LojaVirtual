using LojaVirtual.Models;
using LojaVirtual.Repository;
using Newtonsoft.Json;
using System.Text.Json;

namespace LojaVirtual.Services
{
    public class SessaoService : ISessaoInterface
    {
        private readonly IHttpContextAccessor _httpContext; 
        public SessaoService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public UsuarioModel BuscarSessao()
        {
            string usuarioJson = _httpContext.HttpContext.Session.GetString("UsuarioLogado");
            if (string.IsNullOrEmpty(usuarioJson))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<UsuarioModel>(usuarioJson);
        }

        public void CriarSessao(UsuarioModel usuario)
        {
            string usuarioJson = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext.Session.SetString("UsuarioLogado", usuarioJson);
        }

        public void RemoverSessao()
        {
            _httpContext.HttpContext.Session.Remove("UsuarioLogado");
        }
        
    }
}
