using Microsoft.AspNetCore.Mvc;

namespace ExpresionTreesAddedToASP.Controllers
{
    public class AnotherController : Controller
    {
        public IActionResult SomeAction(int id, string arg)
        {
            return NotFound();
        }
    }
}