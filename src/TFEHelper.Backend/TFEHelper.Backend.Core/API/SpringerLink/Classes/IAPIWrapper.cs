using TFEHelper.Backend.Core.API.SpringerLink.DTO;


namespace TFEHelper.Backend.Core.API.SpringerLink.Classes
{
    /// <summary>
    /// Represents an API consumer
    /// </summary>
    public interface IAPIWrapper
    {
        /// <summary>
        /// Configures the API request.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="publicationDateFrom"></param>
        /// <param name="publicationDateTo"></param>
        /// <param name="subject"></param>
        /// <param name="pageSize"></param>
        void Setup(string query, DateTime? publicationDateFrom, DateTime? publicationDateTo, string? subject, int pageSize = 100);


        /// <summary>
        /// Gets a chunk list of <c>RecordDTO</c> from the API consumer based on provided parameters.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<SpringerLinkRecordDTO>> GetRecordsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a complete list of <c>RecordDTO</c> from the API consumer based on provided parameters.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<SpringerLinkRecordDTO>> GetAllRecordsAsync(CancellationToken cancellationToken = default);
    }
}