using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Repositories;

namespace TFEHelper.Backend.Infrastructure.Database.Implementations
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _applicationsDbContext;
        private readonly IPublicationRepository _publicationRepository;

        public IPublicationRepository Publications => _publicationRepository;

        public RepositoryManager(ApplicationDbContext applicationDbContext, IPublicationRepository publicationRepository)
        {
            _applicationsDbContext = applicationDbContext;
            _publicationRepository = publicationRepository;
        }

        public int Save()
        {
            return _applicationsDbContext.SaveChanges();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await _applicationsDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
