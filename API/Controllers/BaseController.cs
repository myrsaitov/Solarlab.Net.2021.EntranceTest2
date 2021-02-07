using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WidePictBoard.Application.User.Contracts;
using WidePictBoard.Application.User.Service;
using WidePictBoard.Core.Models.User;
using WidePictBoard.Domain.User;

namespace WidePictBoard.Core.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly Func<Exception, IActionResult> _exceptionHandler;
        public BaseController(Func<Exception, IActionResult> exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        protected async Task<IActionResult> ValidateAndRun(Func<Task<IActionResult>> func)
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception e)
            {
                // Need logger here
                
                return _exceptionHandler.Invoke(e);
            }
        }
    }
}