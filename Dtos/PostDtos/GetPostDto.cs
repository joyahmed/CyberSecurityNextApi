namespace CyberSecurityNextApi.Dtos.PostDtos
{
    public class GetPostDto
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public DateTime PostDate { get; set; } = DateTime.Now;

        public string PostTitle { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public ShowEnum TitleonSlide { get; set; }

        public string Subtitle { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ThumbImageUrl { get; set; } = string.Empty;

        public string? SlideImageUrl { get; set; }

        public string? DocumentFileUrl { get; set; }

        public ShowEnum ShowInHomePage { get; set; }

        public PostPublishStatus PublishStatus { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}