using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

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
        Task<int> CountWithTags(
            Expression<Func<Domain.Content, bool>> predicate,
            CancellationToken cancellationToken);
    }
}