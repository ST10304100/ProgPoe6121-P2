using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG_PART_2.Data;

namespace PROG_PART_2.Controllers
{
    // Only accessible to users in the "Coordinator" role
    [Authorize(Roles = "Coordinator")]
    public class CoordinatorController : Controller
    {
        // Dependency injection for the database context
        private readonly ApplicationDBContext _dbContext;

        // Constructor for initializing the database context
        public CoordinatorController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Display all pending claims that need approval from the coordinator
        public IActionResult Index()
        {
            // Retrieve pending claims that haven't been approved by the coordinator yet
            var pendingClaims = _dbContext.Claims
                .Include(c => c.ApplicationUser) // Include associated user data
                .Include(c => c.Documents) // Include associated documents
                .Where(c => !c.IsApprovedByCoordinator && c.Status == "Pending") // Filter for pending claims
                .ToList();

            // Pass the list of pending claims to the view
            return View(pendingClaims);
        }

        // POST: Approve a claim by its ID
        [HttpPost]
        public async Task<IActionResult> Approve(int claimId)
        {
            // Find the claim by its ID
            var claim = await _dbContext.Claims.FindAsync(claimId);

            // If the claim is found, update its status to approved
            if (claim != null)
            {
                claim.IsApprovedByCoordinator = true;
                claim.Status = "Approved by Coordinator"; // Update the claim's status
                await _dbContext.SaveChangesAsync(); // Save the changes to the database
            }

            // Redirect back to the index view to show updated list of claims
            return RedirectToAction("Index");
        }

        // POST: Reject a claim by its ID
        [HttpPost]
        public async Task<IActionResult> Reject(int claimId)
        {
            // Find the claim by its ID
            var claim = await _dbContext.Claims.FindAsync(claimId);

            // If the claim is found, update its status to rejected
            if (claim != null)
            {
                claim.Status = "Rejected by Coordinator"; // Update the claim's status
                await _dbContext.SaveChangesAsync(); // Save the changes to the database
            }

            // Redirect back to the index view to show updated list of claims
            return RedirectToAction("Index");
        }
    }
}
