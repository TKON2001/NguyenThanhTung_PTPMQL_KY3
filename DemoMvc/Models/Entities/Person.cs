using System.ComponentModel.DataAnnotations;

namespace DemoMvc.Models.Entities
{
    public class Person
    {
        [Key]
        public string PersonID { get; set; } = default!;
        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; } = default!;
        public string? Address { get; set; }
        public string? Email { get; set; }
    }
}