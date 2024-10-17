using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PROG_PART_2.Models
{
    public class Document
    {
        // Unique identifier for the document
        public int DocumentId { get; set; }

        // Required: Name of the document, maximum length of 255 characters
        [Required(ErrorMessage = "Document Name is required.")]
        [MaxLength(255)]
        public string DocumentName { get; set; }

        // Required: Path where the document is stored
        [Required(ErrorMessage = "File Path is required.")]
        public string FilePath { get; set; }

        // Required: Date the document was uploaded
        [Required]
        public DateTime UploadedOn { get; set; }

        // Foreign key linking to the associated Claim
        [ForeignKey("Claim")]
        public int ClaimId { get; set; }

        // Navigation property to access the related Claim
        public virtual Claim Claim { get; set; }
    }
}
