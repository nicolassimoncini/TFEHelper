using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Repositories;

namespace TFEHelper.Backend.Infrastructure.Database.Implementations
{
    public class PublicationRepository : BaseRepository<Publication>, IPublicationRepository
    {
        public PublicationRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {            
        }
    }
}
