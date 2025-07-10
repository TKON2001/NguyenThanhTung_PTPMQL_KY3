using DemoMvc.Models;
using DemoMvc.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DemoMvc.Models.Entities.Student> Students { get; set; } = null!;
        public DbSet<DemoMvc.Models.Entities.Person> Persons { get; set; } = null!;
        public DbSet<DemoMvc.Models.Employee> Employees { get; set; } = null!;
        public DbSet<DemoMvc.Models.HeThongPhanPhoi> HeThongPhanPhoi { get; set; } = default!;
        public DbSet<DemoMvc.Models.DaiLy> DaiLy { get; set; } = default!;
    }
}