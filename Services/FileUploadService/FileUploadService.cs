namespace CyberSecurityNextApi.Services.FileUploadService
{
    public class FileUploadService : IFileUploadService
    {
        public async Task<string> UploadFileAsync(string rootPath, IFormFile? file, string subDirectory, string fileNamePrefix)
        {
            if (file != null)
            {
                string fileName = fileNamePrefix + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(rootPath, subDirectory);

                using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return Path.Combine(subDirectory, fileName).Replace("\\", "/");
            }

            return "File couldn't be uploaded!!!";
        }
    }
}
