using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IPublicationRepository Publications { get; }
        int Save();
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
    }
}
