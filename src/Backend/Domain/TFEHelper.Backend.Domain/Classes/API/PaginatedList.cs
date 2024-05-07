using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Classes.API
{
    public class PaginatedList<T>
    {
        public Metadata Metadata { get; set; } = new Metadata();
        public List<T> Items { get; set; } = new List<T>();

        public PaginatedList() { }

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Metadata = new Metadata
            {
                TotalCount = count,
                PageSize = pageSize,
                PageId = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)  // Por ejemplo 1.5 lo transforma en 2
            };
            Items.AddRange(items);
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
