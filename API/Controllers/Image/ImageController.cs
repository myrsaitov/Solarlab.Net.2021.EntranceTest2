using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace SL2021.API.Controllers.Image
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id:int}/forms/{formId:int}")]
        [ProducesResponseType(typeof(ImageFormResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ImageFormResponse> ViewForm(int id, int formId)
        {
            _logger.LogInformation($"viewing the form#{formId} for Content ID={id}");
            await Task.Delay(1000);
            return new ImageFormResponse { FormId = formId, ContentId = id };
        }

        [HttpPost("{id:int}/forms")]
        [ProducesResponseType(typeof(ImageFormResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<ImageFormResponse>> SubmitForm(int id, [FromForm] ImageFormRequest form)
        {
            _logger.LogInformation($"Validating the form#{form.FormId} for Content ID={id}");

            if (form.ImageFile == null || form.ImageFile.Length < 1)
            {
                return BadRequest("The uploaded file is empty.");
            }

            var filePath = Path.Combine(@"App_Data", $"{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(form.ImageFile.FileName)}");
            new FileInfo(filePath).Directory?.Create();
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                _logger.LogInformation($"Saving file [{form.ImageFile.FileName}]");
                await form.ImageFile.CopyToAsync(stream);
                _logger.LogInformation($"\t The uploaded file is saved as [{filePath}].");
            }

            var result = new ImageFormResponse { FormId = form.FormId, ContentId = id, FileSize = form.ImageFile.Length };
            return CreatedAtAction(nameof(ViewForm), new { id, form.FormId }, result);
        }

        /// <summary>
        /// An Example API Endpoint Accepting Multiple Files
        /// </summary>
        /// <param name="id"></param>
        /// <param name="certificates"></param>
        /// <returns></returns>
        [HttpPost("{id:int}/certificates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<List<CertificateResponse>>> SubmitCertificates(int id, [Required] List<IFormFile> certificates)
        {
            var result = new List<CertificateResponse>();

            if (certificates == null || certificates.Count == 0)
            {
                return BadRequest("No file is uploaded.");
            }

            foreach (var certificate in certificates)
            {
                var filePath = Path.Combine(@"App_Data", id.ToString(), @"Certificates", certificate.FileName);
                new FileInfo(filePath).Directory?.Create();

                await using var stream = new FileStream(filePath, FileMode.Create);
                await certificate.CopyToAsync(stream);
                _logger.LogInformation($"The uploaded file [{certificate.FileName}] is saved as [{filePath}].");

                result.Add(new CertificateResponse { FileName = certificate.FileName, FileSize = certificate.Length });
            }

            return Ok(result);
        }

        [HttpGet("files/{id:int}")]
        public async Task<ActionResult> DownloadFile(int id)
        {
            // validation and get the file

            var filePath = $"{id}.txt";
            if (!System.IO.File.Exists(filePath))
            {
                await System.IO.File.WriteAllTextAsync(filePath, "Hello World!");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }
    }

    public class ImageFormRequest
    {
        [Required] public int FormId { get; set; }
        [Required] public IFormFile ImageFile { get; set; }
    }

    public class ImageFormResponse
    {
        public int ContentId { get; set; }
        public int FormId { get; set; }
        public long FileSize { get; set; }
    }

    public class CertificateResponse
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
    }
}
