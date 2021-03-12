using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Domain;

namespace WidePictBoard.Infrastructure.DataAccess.Repositories
{
    public sealed class InMemoryRepository :
        IRepository<Content, int>,
        IRepository<User, string>
    {
        private readonly ConcurrentDictionary<string, User> _users = new();
        private readonly ConcurrentDictionary<int, Content> _contents = new();

        async Task<Content> IRepository<Content, int>.FindById(int id, CancellationToken cancellationToken)
        {
            if (_contents.TryGetValue(id, out var ad))
            {
                _users.TryGetValue(ad.OwnerId, out var user);
                ad.Owner = user;
                return ad;
            }

            return null;
        }

        public async Task Save(User entity, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
            }

            _users.AddOrUpdate(entity.Id, (e) => entity, (i, user) => entity);
        }

        public async Task<User> FindWhere(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _users.Select(pair => pair.Value).Where(compiled).FirstOrDefault();
        }

        async Task<int> IRepository<User, string>.Count(CancellationToken cancellationToken)
        {
            return _users.Count;
        }

        public async Task<int> Count(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _users.Select(pair => pair.Value).Where(compiled).Count();
        }

        async Task<IEnumerable<User>> IRepository<User, string>.GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return _users
                .Select(pair => pair.Value)
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit);
        }

        public async Task<IEnumerable<User>> GetPaged(Expression<Func<User, bool>> predicate, int offset, int limit, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _users
                .Select(pair => pair.Value)
                .OrderBy(u => u.Id)
                .Where(compiled)
                .Skip(offset)
                .Take(limit);
        }

        public async Task Save(Content entity, CancellationToken cancellationToken)
        {
            if (entity.Id == 0)
            {
                entity.Id = Guid.NewGuid().GetHashCode();
            }

            _contents.AddOrUpdate(entity.Id, (e) => entity, (i, user) => entity);
        }

        public async Task<Content> FindWhere(Expression<Func<Content, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _contents.Select(pair => pair.Value).Where(compiled).FirstOrDefault();
        }

        async Task<User> IRepository<User, string>.FindById(string id, CancellationToken cancellationToken)
        {
            if (_users.TryGetValue(id, out var user))
            {
                return user;
            }

            return null;
        }

        async Task<int> IRepository<Content, int>.Count(CancellationToken cancellationToken)
        {
            return _contents.Count;
        }

        public async Task<int> Count(Expression<Func<Content, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _contents.Select(pair => pair.Value).Where(compiled).Count();
        }

        async Task<IEnumerable<Content>> IRepository<Content, int>.GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return _contents
                .Select(pair => pair.Value)
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit);
        }

        public async Task<IEnumerable<Content>> GetPaged(Expression<Func<Content, bool>> predicate, int offset, int limit, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _contents
                .Select(pair => pair.Value)
                .OrderBy(u => u.Id)
                .Where(compiled)
                .Skip(offset)
                .Take(limit);
        }
    }
}