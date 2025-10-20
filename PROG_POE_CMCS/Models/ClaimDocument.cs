using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG_POE_CMCS.Models
{
    public class ClaimDocument
    {
        public int Id { get; set; }

        [Required]
        public int ClaimId { get; set; }

        [ForeignKey("ClaimId")]
        public Claim Claim { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        public string ContentType { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}
