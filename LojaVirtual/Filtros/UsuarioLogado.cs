using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace LojaVirtual.Filtros
{
    public class UsuarioLogado : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            string sessao = context.HttpContext.Session.GetString("UsuarioLogado");

            if (string.IsNullOrEmpty(sessao))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller","Login" },
                    {"action","Login" }
                });

            }
            else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessao);

                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"controller","Login" },
                        {"action","Login" }
                    });
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
