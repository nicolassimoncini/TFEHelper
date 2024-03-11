using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Infrastructure.Database.Interfaces;

namespace TFEHelper.Backend.Infrastructure.Database.Implementations
{
    public class PublicationRepository : BaseRepository<Publication>, IPublicationRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public PublicationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Publication> UpdateAsync(Publication entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Publications.Update(entity);
            await SaveAsync(cancellationToken);
            return entity;
        }
    }
}
