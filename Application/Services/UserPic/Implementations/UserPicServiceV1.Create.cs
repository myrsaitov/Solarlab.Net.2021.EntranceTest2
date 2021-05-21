using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.UserPic.Contracts;
using SL2021.Application.Services.UserPic.Contracts.Exceptions;
using SL2021.Application.Services.UserPic.Interfaces;

namespace SL2021.Application.Services.UserPic.Implementations
{
    public sealed partial class UserPicServiceV1 : IUserPicService
    {
        public async Task<Create.Response> Create(
            Create.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

            if (!ImageExtensions.Contains(Path.GetExtension(request.Image.FileName).ToUpperInvariant()))
            {
                throw new NotAnImageException();
            }

            var fileName = $"{request.UserName}{Path.GetExtension(request.Image.FileName)}";
            var filePath = Path.Combine(@"Images", @"Users", fileName);
            new FileInfo(filePath).Directory?.Create();

            await using var stream = new FileStream(filePath, FileMode.Create);
            await request.Image.CopyToAsync(stream);

            var result = new Create.Response()
            {
                FileName = fileName,
                FileSize = request.Image.Length
            };

            return result;
        }
    }
}