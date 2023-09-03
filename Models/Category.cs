namespace CyberSecurityNextApi.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string Menu { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        public CategoryStatus IsActive { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
