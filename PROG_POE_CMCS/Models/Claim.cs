using System.ComponentModel.DataAnnotations;

namespace PROG_POE_CMCS.Models
{
    public class Claim
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public int HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public string Notes { get; set; }
        public string Condition { get; set; }
        public ICollection<ClaimDocument> Documents { get; set; }
    }
}
