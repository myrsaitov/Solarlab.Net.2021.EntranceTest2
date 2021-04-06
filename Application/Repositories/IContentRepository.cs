using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Domain;

namespace WidePictBoard.Application.Repositories
{
    public interface IContentRepository : IRepository<Domain.Content, int>
    {
        Task<Domain.Content> FindByIdWithUserInclude(
            int id, 
            CancellationToken cancellationToken);
        Task<Domain.Content> FindByIdWithUserAndCategoryAndTags(
            int id, 
            CancellationToken cancellationToken);
        Task<IEnumerable<Content>> GetPagedWithTagsInclude(
            int offset,
            int limit,
            CancellationToken cancellationToken);
        Task<IEnumerable<Content>> GetPagedWithTagsInclude(
            string tag,
            int offset,
            int limit,
            CancellationToken cancellationToken);
    }
}