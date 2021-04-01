using WidePictBoard.Application.Repositories;
using WidePictBoard.Domain;

namespace WidePictBoard.Infrastructure.DataAccess.Repositories
{
    public sealed class TagContentRepository : EfRepository<TagContent, int>, ITagContentRepository
    {
        public TagContentRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }
    }
}