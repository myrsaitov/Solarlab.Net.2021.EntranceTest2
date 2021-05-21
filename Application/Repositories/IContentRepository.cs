using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Domain;

namespace SL2021.Application.Repositories
{
    public interface IContentRepository : IRepository<Domain.Content, int>
    {
        Task<Domain.Content> FindByIdWithUserInclude(
            int id, 
            CancellationToken cancellationToken);
        Task<Domain.Content> FindByIdWithUserAndTagsInclude(
            int id,
            CancellationToken cancellationToken);
        Task<Domain.Content> FindByIdWithUserAndCategoryAndTags(
            int id, 
            CancellationToken cancellationToken);
        Task<Domain.Content> FindByIdWithUserAndImages(
    int id,
    CancellationToken cancellationToken);
        Task<int> CountWithOutDeleted(CancellationToken cancellationToken);
        Task<int> CountWithOutDeleted(
            Expression<Func<Content, bool>> predicate,
            CancellationToken cancellationToken);
        Task<IEnumerable<Content>> GetPagedWithTagsAndOwnerAndCategoryInclude(
            int offset,
            int limit,
            CancellationToken cancellationToken);
        Task<IEnumerable<Content>> GetPagedWithTagsAndOwnerAndCategoryInclude(
            Expression<Func<Content, bool>> predicate,
            int offset,
            int limit,
            CancellationToken cancellationToken);
    }
}