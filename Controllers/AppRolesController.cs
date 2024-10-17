using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PROG_PART_2.Controllers
{
    // Controller for managing application roles
    public class AppRolesController : Controller
    {
        // Role manager to handle roles in ASP.NET Core Identity
        private readonly RoleManager<IdentityRole> _roleManager;

        // Constructor that injects the RoleManager dependency
        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // Action method to display the list of roles
        public IActionResult Index()
        {
            // Fetch all roles and pass them to the view
            var roles = _roleManager.Roles;
            return View(roles);
        }

        // GET method for rendering the role creation form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST method for creating a new role
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            // Check if the role already exists
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                // If the role doesn't exist, create it
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            // Redirect back to the Index action after role creation
            return RedirectToAction("Index");
        }
    }
}
