using System;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Image.Contracts;
using SL2021.Application.Services.Image.Interfaces;

namespace SL2021.Application.Services.Image.Implementations
{
    public sealed partial class ImageServiceV1 : IImageService
    {
        public async Task<Create.Response> Create(
            Create.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }


            return new Create.Response
            {
                Id = 1
            };
        }
    }
}