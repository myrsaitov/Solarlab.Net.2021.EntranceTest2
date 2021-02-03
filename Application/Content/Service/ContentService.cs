using WidePictBoard.Domain.Content;
namespace WidePictBoard.Application.Content.Service
{
    public class ContentService : IContentService
    {
        private readonly IRepository<Domain.Content.Content, string> _repository;

        public ContentService(IRepository<Domain.Content.Content, string> repository)
        {
            _repository = repository;
        }
    }
}