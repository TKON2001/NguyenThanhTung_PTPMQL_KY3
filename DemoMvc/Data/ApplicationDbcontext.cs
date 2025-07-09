// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using YourProjectName.Models; // Thay YourProjectName bằng tên dự án của bạn

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Person { get; set; }
    public DbSet<Employee> Employees { get; set; } // Đổi tên thành Employees (số nhiều) để rõ ràng hơn
}