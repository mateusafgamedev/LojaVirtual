
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
