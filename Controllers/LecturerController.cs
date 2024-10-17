using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG_PART_2.Data;

namespace PROG_PART_2.Controllers
{
    // Restrict access to users with the "Lecturer" role
    [Authorize(Roles = "Lecturer")]
    public class LecturerController : Controller
    {
        // Database context and user manager for handling data and user information
        private readonly ApplicationDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        // Constructor to initialize the database context and user manager
        public LecturerController(ApplicationDBContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        // Action to display the lecturer's claims with optional filtering by date range
        public async Task<IActionResult> Claims(DateTime? start, DateTime? end)
        {
            // Get the currently logged-in user
            var currentUser = await _userManager.GetUserAsync(User);
            var currentUserId = await _userManager.GetUserIdAsync(currentUser);

            // Query to retrieve claims submitted by the current lecturer
            var query = _dbContext.Claims
                .Include(c => c.Documents) // Include related documents
                .Where(c => c.ApplicationUserId == currentUserId); // Filter by current user's claims

            // Apply date filtering if both start and end dates are provided
            if (start.HasValue && end.HasValue)
            {
                query = query.Where(c => c.DateSubmitted >= start.Value && c.DateSubmitted <= end.Value);
            }

            // Execute the query and retrieve the list of claims
            var claims = await query.ToListAsync();

            // Return the list of claims to the view
            return View(claims);
        }
    }
}
