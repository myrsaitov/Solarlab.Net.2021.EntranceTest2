using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Content.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace SL2021.API.Controllers.Content
{
    public partial class ContentController
    {
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Restore(int id, CancellationToken cancellationToken)
        {
            await _contentService.Restore(new Restore.Request
            {
                Id = id
            }, cancellationToken);
            
            return NoContent();
        }
    }
}