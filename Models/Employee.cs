using System.ComponentModel.DataAnnotations;
namespace EmployeeInfoManagement.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name can only contain letters.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Department { get; set; }

        [Required]
        public DateTime HireDate { get; set; }
    }

}
