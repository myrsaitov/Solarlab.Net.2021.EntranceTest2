using SL2021.Application.Repositories;
using SL2021.Domain;

namespace SL2021.Infrastructure.DataAccess.Repositories
{
    public sealed class ImageRepository : EfRepository<Image, int>, IImageRepository
    {
        public ImageRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }
    }
}