using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Repositories;
using SL2021.Domain;
using Microsoft.EntityFrameworkCore;

namespace SL2021.Infrastructure.DataAccess.Repositories
{
    public sealed class TagRepository : EfRepository<Tag, int>, ITagRepository
    {
        public TagRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }
    }
}