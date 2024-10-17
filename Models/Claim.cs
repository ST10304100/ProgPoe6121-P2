using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PROG_PART_2.Models
{
    public class Claim
    {
        // Unique identifier for the claim
        public int ClaimId { get; set; }

        // Required: Number of hours worked, must be between 1 and 100
        [Required(ErrorMessage = "Hours Worked is required.")]
        [Range(1, 100, ErrorMessage = "Hours Worked must be between 1 and 100.")]
        public decimal HoursWorked { get; set; }

        // Required: Hourly rate for the work done, must be between 50 and 1000
        [Required(ErrorMessage = "Hourly Rate is required.")]
        [Range(50, 1000, ErrorMessage = "Hourly Rate must be between 50 and 1000.")]
        public decimal HourlyRate { get; set; }

        // Required: Total amount calculated based on hours worked and hourly rate
        [Required]
        public decimal TotalAmount { get; set; }

        // Optional: Notes regarding the claim, max length of 500 characters
        [MaxLength(500, ErrorMessage = "Notes can't exceed 500 characters.")]
        public string Notes { get; set; }

        // Required: Date when the claim was submitted, validated with a custom validation method
        [Required]
        [CustomValidation(typeof(Claim), nameof(ValidateSubmissionDate))]
        public DateTime DateSubmitted { get; set; }

        // Status of the claim, default is "Pending"
        public string Status { get; set; } = "Pending";

        // Flags indicating whether the claim is approved by the coordinator or manager
        public bool IsApprovedByCoordinator { get; set; } = false;
        public bool IsApprovedByManager { get; set; } = false;

        // Foreign key to associate the claim with an application user
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        // Navigation property to the associated application user
        public virtual ApplicationUser ApplicationUser { get; set; }

        // Collection of documents associated with the claim
        public virtual ICollection<Document> Documents { get; set; }

        // Custom validation method for submission date
        public static ValidationResult ValidateSubmissionDate(DateTime dateSubmitted, ValidationContext context)
        {
            var currentDate = DateTime.Now;
            // Check if the submission date is in the future
            if (dateSubmitted > currentDate)
            {
                return new ValidationResult("Date Submitted cannot be in the future.");
            }

            // Check if the submission date is from the current or previous month
            if (dateSubmitted.Month != currentDate.Month && dateSubmitted.Month != currentDate.AddMonths(-1).Month)
            {
                return new ValidationResult("Date Submitted can only be from the current month or previous month.");
            }

            return ValidationResult.Success; // Validation passed
        }
    }
}
