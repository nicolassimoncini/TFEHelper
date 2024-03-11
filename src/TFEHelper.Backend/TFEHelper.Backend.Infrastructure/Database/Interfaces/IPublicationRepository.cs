using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Classes.API.Specifications;
using TFEHelper.Backend.Domain.Classes.Models;


namespace TFEHelper.Backend.Infrastructure.Database.Interfaces
{
    public interface IPublicationRepository : IBaseRepository<Publication>
    {
        Task<Publication> UpdateAsync(Publication publication, CancellationToken cancellationToken = default);
    }
}
