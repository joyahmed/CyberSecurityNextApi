namespace CyberSecurityNextApi.Services.FileUploadService
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(string rootPath, IFormFile? file, string subDirectory, string fileNamePrefix);
    }
}