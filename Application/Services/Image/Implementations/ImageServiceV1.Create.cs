using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Microsoft.AspNetCore.Http;
using SL2021.Application.Common;
using SL2021.Application.Services.Content.Contracts.Exceptions;
using SL2021.Application.Services.Image.Contracts;
using SL2021.Application.Services.Image.Interfaces;
using SL2021.Application.Services.Images.Contracts.Exceptions;
using SL2021.Domain.General.Exceptions;

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

            if (request.Images.Count == 0)
            {
                throw new NoImagesException();
            }

            var result = new Create.Response();

            var content = await _contentRepository.FindByIdWithUserAndImages(
                request.Id,
                cancellationToken);

            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            if (content.Images == null)
            {
                content.Images = new List<Domain.Image>();
            }

            string userId = await _identityService.GetCurrentUserId(cancellationToken);
            var isAdmin = await _identityService.IsInRole(
                userId,
                RoleConstants.AdminRole,
                cancellationToken);

            if (!isAdmin && content.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }



            var ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

            foreach (var image in request.Images)
            {
                if (ImageExtensions.Contains(Path.GetExtension(image.FileName).ToUpperInvariant()))
                {
                    var domain_image = new Domain.Image()
                    {
                        // NuGet Flurl.Http
                        //URL = $"https://localhost:44377/api/v1/images/contents/1/IMG_20180722_102430_HDR.jpg",
                        URL = Url.Combine(
                            // TODO Get Current Domain
                            "https://localhost:44377", 
                            "api/v1/images/contents", 
                            request.Id.ToString(), 
                            image.FileName),

                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false
                    };

                    var filePath = Path.Combine(@"Images", @"Contents", request.Id.ToString(), image.FileName);
                    new FileInfo(filePath).Directory?.Create();

                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);

                    result.Add(new UploadImageResponse { FileName = image.FileName, FileSize = image.Length });
                    content.Images.Add(domain_image);
                }
            }

            await _contentRepository.Save(
                content,
                cancellationToken);

            return result;
        }
    }
}