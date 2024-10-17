using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG_PART_2.Data;

namespace PROG_PART_2.Controllers
{
    // Restrict access to users with the "Manager" role
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        // Database context to interact with the database
        private readonly ApplicationDBContext _context;

        // Constructor to initialize the database context
        public ManagerController(ApplicationDBContext context)
        {
            _context = context;
        }

        // Action to display claims approved by the coordinator but pending manager approval
        public IActionResult Index()
        {
            // Retrieve all claims approved by the coordinator but not yet approved by the manager
            var pendingClaims = _context.Claims
                .Include(c => c.ApplicationUser) // Include related user information
                .Include(c => c.Documents) // Include associated documents
                .Where(c => c.IsApprovedByCoordinator && !c.IsApprovedByManager && c.Status == "Approved by Coordinator")
                .ToList();

            // Return the list of pending claims to the view
            return View(pendingClaims);
        }

        // Action to approve a claim by the manager
        [HttpPost]
        public async Task<IActionResult> Approve(int claimId)
        {
            // Find the claim by its ID
            var claim = await _context.Claims.FindAsync(claimId);

            // If the claim exists, update its status and mark it as approved by the manager
            if (claim != null)
            {
                claim.IsApprovedByManager = true;
                claim.Status = "Approved by Manager";
                await _context.SaveChangesAsync(); // Save changes to the database
            }

            // Redirect back to the index view
            return RedirectToAction("Index");
        }

        // Action to reject a claim by the manager
        [HttpPost]
        public async Task<IActionResult> Reject(int claimId)
        {
            // Find the claim by its ID
            var claim = await _context.Claims.FindAsync(claimId);

            // If the claim exists, update its status to rejected by the manager
            if (claim != null)
            {
                claim.Status = "Rejected by Manager";
                await _context.SaveChangesAsync(); // Save changes to the database
            }

            // Redirect back to the index view
            return RedirectToAction("Index");
        }
    }
}
