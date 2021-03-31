using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;

namespace WidePictBoard.Infrastructure.DataAccess.Repositories
{
    public sealed class TagRepository : EfRepository<Category, int>, ITagRepository
    {
        public TagRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }
    }
}