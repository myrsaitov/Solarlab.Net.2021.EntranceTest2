﻿using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Content.Contracts;

namespace SL2021.API.Controllers.Content
{
    public partial class ContentController
    {
        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> Update(
            int id, 
            ContentUpdateRequest request, 
            CancellationToken cancellationToken)
        {
            var response = await _contentService.Update(new Update.Request
            {
                Id = id,
                Title = request.Title,
                CongratulationsText = request.CongratulationsText,
                CategoryId = request.CategoryId,
                TagBodies = request.Tags
            }, cancellationToken);

            return NoContent();
        }

        public sealed class ContentUpdateRequest
        {
            [Required]
            [MaxLength(100)]
            public string Title { get; set; }

            [Required]
            [MaxLength(1000)]
            public string CongratulationsText { get; set; }

            [Required]
            [Range(1, 100_000_000_000)]
            public int CategoryId { get; set; }

            public string[] Tags { get; set; }
        }
    }
}