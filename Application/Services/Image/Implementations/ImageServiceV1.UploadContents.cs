using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Image.Contracts;
using SL2021.Application.Services.Image.Interfaces;
using SL2021.Application.Services.Images.Contracts.Exceptions;

namespace SL2021.Application.Services.Image.Implementations
{
    public sealed partial class ImageServiceV1 : IImageService
    {
        public async Task<UploadContents.Response> UploadContents(
            UploadContents.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Images.Count == 0)
            {
                throw new UploadNoImagesException();
            }

            var result = new UploadContents.Response();

            var content = await _contentRepository.FindByIdWithUserAndCategoryAndTags(
                request.Id,
                cancellationToken);


            var ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

            foreach (var image in request.Images)
            {
                if (ImageExtensions.Contains(Path.GetExtension(image.FileName).ToUpperInvariant()))
                {
                    var filePath = Path.Combine(@"Images", @"Contents", request.Id.ToString(), image.FileName);
                    new FileInfo(filePath).Directory?.Create();

                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);

                    result.Add(new UploadImage.Response { FileName = image.FileName, FileSize = image.Length });
                    content.ImageURLs.Add("ImageURL");
                }
            }

            await _contentRepository.Save(
                content,
                cancellationToken);

            return result;
        }
        public async Task<UploadUser.Response> UploadUser(
            UploadUser.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

            if (!ImageExtensions.Contains(Path.GetExtension(request.Image.FileName).ToUpperInvariant()))
            {
                throw new UploadNotAnImageException();
            }

            var fileName = $"{request.UserName}{Path.GetExtension(request.Image.FileName)}";
            var filePath = Path.Combine(@"Images", @"Users", fileName);
            new FileInfo(filePath).Directory?.Create();

            await using var stream = new FileStream(filePath, FileMode.Create);
            await request.Image.CopyToAsync(stream);

            var result = new UploadUser.Response()
            {
                FileName = fileName,
                FileSize = request.Image.Length
            };

            return result;
        }
    }
}