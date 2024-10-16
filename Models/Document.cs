﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PROG_PART_2.Models
{
    public class Document
    {
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Document Name is required.")]
        [MaxLength(255)]
        public string DocumentName { get; set; }

        [Required(ErrorMessage = "File Path is required.")]
        public string FilePath { get; set; }

        [Required]
        public DateTime UploadedOn { get; set; }

        
    }
}
