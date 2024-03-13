using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Enums;

namespace TFEHelper.Backend.Core.Engine.Interfaces
{
    public interface ITFEHelperEngine
    {
        Task ImportPublicationsAsync(string filePath, FileFormatType formatType, SearchSourceType source, bool discardInvalidRecords = true, CancellationToken cancellationToken = default);

        Task ExportPublicationsAsync(List<Publication> publications, string filePath, FileFormatType formatType, CancellationToken cancellationToken = default);
    }
}
