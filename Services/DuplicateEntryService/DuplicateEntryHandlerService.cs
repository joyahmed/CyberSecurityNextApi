using Microsoft.Data.SqlClient;

namespace CyberSecurityNextApi.Services.DuplicateEntryService
{
    public class DuplicateEntryHandlerService : IDuplicateEntryHandler
    {
        public string? GetDuplicateEntryErrorMessage(DbUpdateException ex)
        {
            var sqlException = ex.InnerException as SqlException;

            if (sqlException != null)
            {
                // Extract the duplicated values from the error message
                string errorMessage = sqlException.Message;
                int startIndex = errorMessage.IndexOf("(") + 1;
                int endIndex = errorMessage.IndexOf(")");
                string duplicateValues = errorMessage.Substring(startIndex, endIndex - startIndex);

                string errorDescription = endIndex > startIndex ? "Duplicate Values" : "Duplicate Value";

                return $"Duplicate Value: {duplicateValues}";
            }

            return null; // No duplicate entry error
        }
    }

}
