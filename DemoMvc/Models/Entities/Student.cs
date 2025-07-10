using System.ComponentModel.DataAnnotations;

namespace DemoMvc.Models.Entities
{
    public class Student
    {
        [Key]
        public required string StudentID { get; set; }
        [MinLength(5, ErrorMessage = "Full name must be at least 5 characters long.")]
        public required string FullName { get; set; }
        public string? Address { get; set; }
    }
}