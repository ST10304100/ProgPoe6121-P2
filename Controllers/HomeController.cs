using Microsoft.AspNetCore.Mvc;
using PROG_PART_2.Models;
using System.Diagnostics;

namespace PROG_PART_2.Controllers
{
    public class HomeController : Controller
    {
        // Injecting a logger to log information, warnings, or errors in the controller
        private readonly ILogger<HomeController> _logger;

        // Constructor to initialize the logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action to display the home page (Index view)
        public IActionResult Index()
        {
            return View(); // Renders the Index view
        }

        // Error handling action that returns an error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Creates a new ErrorViewModel with a unique RequestId for tracking errors
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
