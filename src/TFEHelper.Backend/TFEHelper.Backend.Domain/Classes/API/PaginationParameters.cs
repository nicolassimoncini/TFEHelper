using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Classes.API
{
    public class PaginationParameters
    {
        public int PageNumber { get; set; }  // Numero de Pagina
        public int PageSize { get; set; }   // Cuantos registros por pagina
    }
}
