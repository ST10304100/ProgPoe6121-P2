using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG_PART_2.Data;
using PROG_PART_2.Models;

namespace PROG_PART_2.Controllers
{
    // Only accessible to users in the "Lecturer" role
    [Authorize(Roles = "Lecturer")]
    public class ClaimController : Controller
    {
        // Dependency injection for database context, user manager, and hosting environment
        private readonly ApplicationDBContext _databaseContext;
        private readonly UserManager<IdentityUser> _accountManager;
        private readonly IWebHostEnvironment _webEnvironment;

        // Constructor for initializing the dependencies
        public ClaimController(ApplicationDBContext databaseContext, UserManager<IdentityUser> accountManager, IWebHostEnvironment webEnvironment)
        {
            _databaseContext = databaseContext;
            _accountManager = accountManager;
            _webEnvironment = webEnvironment;
        }

        // GET: Render the Create Claim form
        public IActionResult Create()
        {
            return View();
        }

        // POST: Handle the submission of a new claim
        [HttpPost]
        [ValidateAntiForgeryToken] // Security measure to prevent cross-site request forgery
        public async Task<IActionResult> Create(ClaimViewModel viewModel)
        {
            // If the form data is invalid, return the same view with validation errors
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Validate that supporting documents are attached
            if (viewModel.SupportingDocuments == null || viewModel.SupportingDocuments.Count == 0)
            {
                ModelState.AddModelError("", "At least one supporting document must be attached.");
                return View(viewModel);
            }

            // Validate file types and size
            bool invalidFileDetected = false;
            foreach (var file in viewModel.SupportingDocuments)
            {
                if (file.ContentType != "application/pdf" || file.Length > 15 * 1024 * 1024) // Only allow PDF under 15 MB
                {
                    ViewBag.FileError = true;
                    invalidFileDetected = true;
                    ModelState.AddModelError("", "Only PDF files under 15 MB are allowed.");
                    return View(viewModel);
                }
            }

            // If all files are valid, proceed with claim creation
            if (!invalidFileDetected)
            {
                // Get the currently logged-in user
                var loggedInUser = await _accountManager.GetUserAsync(User);

                // Create a new Claim object with user data
                var newClaim = new Claim
                {
                    HoursWorked = viewModel.HoursWorked,
                    HourlyRate = viewModel.HourlyRate,
                    Notes = viewModel.Notes,
                    DateSubmitted = DateTime.Now,
                    ApplicationUserId = loggedInUser.Id,
                    TotalAmount = viewModel.HourlyRate * viewModel.HoursWorked // Calculate total amount
                };

                // Add the new claim to the database and save changes
                _databaseContext.Claims.Add(newClaim);
                await _databaseContext.SaveChangesAsync();

                // Define the directory for file uploads
                var uploadDirectory = Path.Combine(_webEnvironment.WebRootPath, "documents");

                // Save each supporting document
                foreach (var file in viewModel.SupportingDocuments)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName; // Generate a unique file name
                    var filePath = Path.Combine(uploadDirectory, uniqueFileName);

                    // Ensure the upload directory exists
                    Directory.CreateDirectory(uploadDirectory);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    // Add document record to the database
                    var documentRecord = new Document
                    {
                        ClaimId = newClaim.ClaimId,
                        DocumentName = uniqueFileName,
                        FilePath = filePath
                    };

                    _databaseContext.Documents.Add(documentRecord);
                }

                // Save changes after adding the documents
                await _databaseContext.SaveChangesAsync();

                // Display success message and redirect to claims page
                TempData["SuccessMessage"] = "Claim submitted successfully!";
                return RedirectToAction("Claims", "Lecturer");
            }

            // Return the view with any errors
            return View(viewModel);
        }

        // GET: Display the claim status for the logged-in user
        public async Task<IActionResult> ViewClaimStatus()
        {
            // Get the currently logged-in user
            var currentUser = await _accountManager.GetUserAsync(User);

            // Retrieve claims submitted by the logged-in user
            var userClaims = _databaseContext.Claims
                .Where(c => c.ApplicationUserId == currentUser.Id)
                .ToList();

            // Pass the claims to the view
            return View(userClaims);
        }
    }
}
