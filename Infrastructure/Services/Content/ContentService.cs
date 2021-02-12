using WidePictBoard.Application;
using WidePictBoard.Application.Content.Service;

namespace WidePictBoard.Infrastructure.Services.Content
{
    public class ContentService : IContentService
    {
        private readonly IRepository<Domain.Content, string> _repository;

        public ContentService(IRepository<Domain.Content, string> repository)
        {
            _repository = repository;
        }
    }
}