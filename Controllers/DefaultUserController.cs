using Microsoft.AspNetCore.Mvc;

namespace PROG_PART_2.Controllers
{
    public class DefaultUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
