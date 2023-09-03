namespace CyberSecurityNextApi.Dtos.Category
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public string Menu { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        public CategoryStatus IsActive { get; set; }
    }
}