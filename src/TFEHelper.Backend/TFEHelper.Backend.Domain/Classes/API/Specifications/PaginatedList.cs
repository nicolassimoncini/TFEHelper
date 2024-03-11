using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Classes.API.Specifications
{
    public class PaginatedList<T> : List<T>
    {

        public Metadata Metadata { get; set; }

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Metadata = new Metadata
            {
                TotalCount = count,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)  // Por ejemplo 1.5 lo transforma en 2
            };
            AddRange(items);
        }

        public static PaginatedList<T> ToPagedList(IEnumerable<T> entity, int pageNumber, int pageSize)
        {
            var count = entity.Count();
            var items = entity.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize).ToList();

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
