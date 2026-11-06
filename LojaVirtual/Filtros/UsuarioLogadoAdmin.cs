using LojaVirtual.Enum;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LojaVirtual.Filtros
{
    public class UsuarioLogadoAdmin : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessao = context.HttpContext.Session.GetString("UsuarioLogado");
            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessao);

            if (usuario.Cargo != CargoEnum.Administrador)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    {"controller","Home" },
                    {"action","Index" }
                });
            }

            base.OnActionExecuting(context);
        }
    }
}
