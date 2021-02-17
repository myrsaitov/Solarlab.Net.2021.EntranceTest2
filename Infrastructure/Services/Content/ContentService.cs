using WidePictBoard.Application;
using WidePictBoard.Application.Content.Interface;
using System.Threading.Tasks;
using WidePictBoard.Application.Content.Contracts;

namespace WidePictBoard.Infrastructure.Services.Content
{
    public class ContentService : IContentService
    {
        private readonly IRepository<Domain.Content, string> _repository;

        public ContentService(IRepository<Domain.Content, string> repository)
        {
            _repository = repository;
        }
        
        
        public async Task<GetById.Response> GetById(GetById.Request request)
        {
            throw new System.NotImplementedException();
        }
        public async Task<GetPaged.Response> GetPaged(GetPaged.Request request)
        {
            throw new System.NotImplementedException();
        }
        public async Task<Delete.Response> Delete(Delete.Request request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Create.Response> Create(Create.Request request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Update.Response> Update(Update.Request request)
        {
            throw new System.NotImplementedException();
        }
        
        
        
    }
}