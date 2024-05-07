using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Services.Contracts.DTO.API
{
    public class PaginatedListDTO<T>
    {
        public MetadataDTO Metadata { get; set; } = new MetadataDTO();
        public List<T> Items { get; set; } = new List<T>();

        public PaginatedListDTO() { }

        public PaginatedListDTO(List<T> items, int count, int pageNumber, int pageSize)
        {
            Metadata = new MetadataDTO
            {
                TotalCount = count,
                PageSize = pageSize,
                PageId = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)  // Por ejemplo 1.5 lo transforma en 2
            };
            Items.AddRange(items);
        }

        public static PaginatedListDTO<T> ToPagedList(IEnumerable<T> entity, int pageNumber, int pageSize)
        {
            var count = entity.Count();
            var items = entity.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize).ToList();

            return new PaginatedListDTO<T>(items, count, pageNumber, pageSize);
        }
    }
}
