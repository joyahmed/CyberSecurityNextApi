namespace CyberSecurityNextApi.Services.SlugService
{
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    public class SlugService : ISlugService
    {
        public string GenerateSlug(string input)
        {
            // Remove leading/trailing spaces and convert to lowercase
            string normalizedTitle = input.Trim().ToLowerInvariant();

            // Replace spaces with hyphens
            normalizedTitle = normalizedTitle.Replace(" ", "-");

            // Remove special characters and replace with hyphens
            normalizedTitle = Regex.Replace(normalizedTitle, "[^a-z0-9\\-]", "");

            // Remove duplicate hyphens
            normalizedTitle = Regex.Replace(normalizedTitle, "-{2,}", "-");

            // Truncate to a reasonable length
            if (normalizedTitle.Length > 100)
            {
                normalizedTitle = normalizedTitle.Substring(0, 100);
            }

            return normalizedTitle;
        }
    }

}
