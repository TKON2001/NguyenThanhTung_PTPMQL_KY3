// Vị trí file: Models/Employee.cs

namespace DemoMvc.Models // Sử dụng namespace của dự án là "DemoMvc"
{
    // Lớp cơ sở Person (nếu bạn có)
    public class Person
    {
        public int Id { get; set; } // Khóa chính tự tăng
        public string? FullName { get; set; }
        public string? Address { get; set; }
    }

    // Lớp Employee kế thừa từ Person
    public class Employee : Person
    {
        // Mã nhân viên được sinh tự động, ví dụ: "ES001"
        public string EmployeeId { get; set; }

        public int Age { get; set; }

        // Bạn có thể thêm các thuộc tính khác cho Employee ở đây
    }
}