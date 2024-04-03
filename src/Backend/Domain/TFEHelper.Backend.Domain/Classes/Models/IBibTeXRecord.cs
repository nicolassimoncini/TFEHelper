using TFEHelper.Backend.Domain.Enums;

namespace TFEHelper.Backend.Domain.Classes.Models
{
    public interface IBibTeXRecord
    {
        public BibTeXPublicationType Type { get; set; }
        public string? Key { get; set; }
    }
}
