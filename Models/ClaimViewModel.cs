using System.ComponentModel.DataAnnotations;

namespace PROG_PART_2.Models
{
    public class ClaimViewModel
    {
        // Required: Number of hours worked, must be between 1 and 100
        [Required(ErrorMessage = "Hours Worked is required.")]
        [Range(1, 100, ErrorMessage = "Hours Worked must be between 1 and 100.")]
        public decimal HoursWorked { get; set; }

        // Required: Hourly rate for the work done, must be between 50 and 1000
        [Required(ErrorMessage = "Hourly Rate is required.")]
        [Range(50, 1000, ErrorMessage = "Hourly Rate must be between 50 and 1000.")]
        public decimal HourlyRate { get; set; }

        // Optional: Notes regarding the claim, max length of 500 characters
        [MaxLength(500, ErrorMessage = "Notes can't exceed 500 characters.")]
        public string Notes { get; set; }

        // Optional: List of supporting documents for the claim, to be uploaded as files
        [Display(Name = "Supporting Documents")]
        public List<IFormFile> SupportingDocuments { get; set; }
    }
}
