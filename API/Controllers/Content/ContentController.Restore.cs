using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Content.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SL2021.API.Controllers.Content
{
    public partial class ContentController
    {
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Restore(
            int id, 
            CancellationToken cancellationToken)
        {
            await _contentService.Restore(new Restore.Request
            {
                Id = id
            }, cancellationToken);
            
            return NoContent();
        }
    }
}