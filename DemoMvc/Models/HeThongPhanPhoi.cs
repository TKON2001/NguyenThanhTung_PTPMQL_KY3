using System.ComponentModel.DataAnnotations;

namespace DemoMvc.Models
{
    public class HeThongPhanPhoi
    {
        [Key]
        public string? MaHTPP { get; set; }
        public string? TenHTPP { get; set; }
    }
}
