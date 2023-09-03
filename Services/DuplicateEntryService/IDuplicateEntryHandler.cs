namespace CyberSecurityNextApi.Services.DuplicateEntryService
{
    public interface IDuplicateEntryHandler
    {
        string? GetDuplicateEntryErrorMessage(DbUpdateException ex);
    }
}
