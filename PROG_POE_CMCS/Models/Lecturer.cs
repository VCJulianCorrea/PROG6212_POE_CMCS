using System.ComponentModel.DataAnnotations;

namespace PROG_POE_CMCS.Models
{
    public class Lecturer
    {
        [Key]
        public int LecturerID { get; set; }
        [Required]
        public string LecturerName { get; set; }

    }
}
