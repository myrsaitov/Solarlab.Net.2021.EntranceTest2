using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Content.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace SL2021.API.Controllers.Content
{
    public partial class ContentController
    {
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _contentService.Delete(new Delete.Request
            {
                Id = id
            }, cancellationToken);
            
            return NoContent();
        }
    }
}