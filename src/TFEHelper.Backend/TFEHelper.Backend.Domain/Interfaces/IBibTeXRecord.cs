using TFEHelper.Backend.Domain.Enums;

namespace TFEHelper.Backend.Domain.Interfaces
{
    public interface IBibTeXRecord
    {
        public BibTeXPublicationType Type { get; set; }
        public string Key { get; set; }
    }
}
