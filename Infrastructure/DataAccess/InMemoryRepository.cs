using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Domain;
using WidePictBoard.Application;
using WidePictBoard.Domain;

namespace WidePictBoard.Infrastructure.DataAccess
{
    public sealed class InMemoryRepository 
        : 
            IRepository<Domain.Content, int>,
            IRepository<Domain.Comment, int>,
            IRepository<Domain.User, int>
    {
        private readonly ConcurrentDictionary<int, User> _users = new();
        private readonly ConcurrentDictionary<int, Content> _ads = new();
        private readonly ConcurrentDictionary<int, Comment> _comments = new();

        async Task<Content> IRepository<Content, int>.FindById(int id, CancellationToken cancellationToken)
        {
            if (_ads.TryGetValue(id, out var ad))
            {
                _users.TryGetValue(ad.OwnerId, out var user);
                ad.Owner = user;
                return ad;
            }

            return null;
        }

        public async Task Save(Comment entity, CancellationToken cancellationToken)
        {
            if (entity.Id == 0)
            {
                entity.Id = Guid.NewGuid().GetHashCode();
            }

            _comments.AddOrUpdate(entity.Id, (e) => entity, (i, user) => entity);
        }

        public async Task<Comment> FindWhere(Expression<Func<Comment, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _comments.Select(pair => pair.Value).Where(compiled).FirstOrDefault();
        }

        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return _comments.Count;
        }

        public async Task<IEnumerable<Comment>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return _comments
                .Select(pair => pair.Value)
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit);
        }

        public async Task Save(User entity, CancellationToken cancellationToken)
        {
            if (entity.Id == 0)
            {
                entity.Id = Guid.NewGuid().GetHashCode();
            }

            _users.AddOrUpdate(entity.Id, (e) => entity, (i, user) => entity);
        }

        public async Task<User> FindWhere(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _users.Select(pair => pair.Value).Where(compiled).FirstOrDefault();
        }

        public async Task<Comment> FindById(int id, CancellationToken cancellationToken)
        {
            if (_comments.TryGetValue(id, out var comment))
            {
                if (_users.TryGetValue(comment.AuthorId, out var user))
                {
                    comment.Author = user;
                }
                return comment;
            }

            return null;
        }

        async Task<int> IRepository<User, int>.Count(CancellationToken cancellationToken)
        {
            return _users.Count;
        }

        public async Task<int> Count(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _users.Select(pair => pair.Value).Where(compiled).Count();
        }

        async Task<IEnumerable<User>> IRepository<User, int>.GetPaged(int offset, int limit, CancellationToken cancellationToken)
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

            _ads.AddOrUpdate(entity.Id, (e) => entity, (i, user) => entity);
        }

        public async Task<Content> FindWhere(Expression<Func<Content, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _ads.Select(pair => pair.Value).Where(compiled).FirstOrDefault();
        }

        async Task<User> IRepository<User, int>.FindById(int id, CancellationToken cancellationToken)
        {
            if (_users.TryGetValue(id, out var user))
            {
                return user;
            }

            return null;
        }

        async Task<int> IRepository<Content, int>.Count(CancellationToken cancellationToken)
        {
            return _ads.Count;
        }

        public async Task<int> Count(Expression<Func<Content, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _ads.Select(pair => pair.Value).Where(compiled).Count();
        }

        async Task<IEnumerable<Content>> IRepository<Content, int>.GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return _ads
                .Select(pair => pair.Value)
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit);
        }

        public async Task<IEnumerable<Content>> GetPaged(Expression<Func<Content, bool>> predicate, int offset, int limit, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _ads
                .Select(pair => pair.Value)
                .OrderBy(u => u.Id)
                .Where(compiled)
                .Skip(offset)
                .Take(limit);
        }
    }
}