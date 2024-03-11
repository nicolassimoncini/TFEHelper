using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Enums;

namespace TFEHelper.Backend.Core.Engine.Interfaces
{
    public interface ITFEEngine
    {
        Task ImportPublicationsAsync(string filePath, FileFormatType formatType, SearchSourceType source, CancellationToken cancellationToken = default);

        Task ExportPublicationsAsync(List<Publication> publications, string filePath, FileFormatType formatType, CancellationToken cancellationToken = default);
    }
}
