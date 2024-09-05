using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Models
{
    public class PatientModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Phone]
        public string ContactNumber { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Weight { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Height { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Address { get; set; }

        public string MedicalComments { get; set; }

        [Required]
        public bool AnyMedicationsTaking { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
