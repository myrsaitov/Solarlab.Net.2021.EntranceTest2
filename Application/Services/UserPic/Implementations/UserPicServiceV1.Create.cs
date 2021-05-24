using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.UserPic.Contracts;
using SL2021.Application.Services.UserPic.Contracts.Exceptions;
using SL2021.Application.Services.UserPic.Interfaces;
using Flurl;  // NuGet Flurl.Http
using SL2021.Application.Services.User.Contracts.Exceptions;

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

            var user = await _userRepository.FindByUserName(
                request.UserName,
                cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(request.UserName);
            }

            var fileName = $"{request.UserName}{Path.GetExtension(request.Image.FileName)}";

            var domainUserPic = new Domain.UserPic()
            {
                URL = Url.Combine(
                    request.BaseURL,
                    "api/v1/images/userpics",
                    fileName),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            user.UserPic = domainUserPic;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.Save(
                user,
                cancellationToken);

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