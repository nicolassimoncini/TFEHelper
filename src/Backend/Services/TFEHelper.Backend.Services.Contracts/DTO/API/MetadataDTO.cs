using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Services.Contracts.DTO.API
{
    public class MetadataDTO
    {
        public int TotalPages { get; set; }  // Totalidad de Paginas
        public int PageSize { get; set; }  // Registros por Pagina
        public int TotalCount { get; set; }  // Totalidad de Registros
    }
}