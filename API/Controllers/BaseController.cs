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
        private readonly Func<Func<Task<IActionResult>>, Task<IActionResult>> _exceptionHandler;
        public BaseController(Func<Func<Task<IActionResult>>, Task<IActionResult>> exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        protected async Task<IActionResult> ValidateAndRun(Func<Task<IActionResult>> func)
        {
            return await _exceptionHandler.Invoke(func);
        }
    }
}