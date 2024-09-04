using Microsoft.AspNetCore.Mvc;

namespace NortonsDomeSimulation.Controllers
{
    public class NortonsDomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
