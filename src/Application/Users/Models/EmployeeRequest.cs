using System.ComponentModel.DataAnnotations;

namespace Employee.Application.Users.Models
{
    public class EmployeeRequest
    {
        [Required]
        [Range(1000, 9999, ErrorMessage = "Employee ID must be a 4-digit number.")]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name can be up to 20 characters.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Name must contain only alphabetic characters.")]
        public string Name { get; set; }

        [Required]
        [Range(10, 99, ErrorMessage = "Age must be a 2-digit number.")]
        public int Age { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Address can be up to 30 characters.")]
        [RegularExpression("^[a-zA-Z0-9 ,.-]+$", ErrorMessage = "Address must be a valid format.")]
        public string Address { get; set; }
    }
}