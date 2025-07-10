// Vị trí file: Models/Employee.cs

namespace DemoMvc.Models
{
    public class Employee : DemoMvc.Models.Entities.Person
    {
        // Mã nhân viên được sinh tự động, ví dụ: "ES001"
        public string? EmployeeId { get; set; }
        public int Age { get; set; }
        // Bạn có thể thêm các thuộc tính khác cho Employee ở đây
    }
}