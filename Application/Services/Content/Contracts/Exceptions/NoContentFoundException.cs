using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Content.Contracts.Exceptions
{
    public sealed class NoContentFoundException : NotFoundException
    {
        public NoContentFoundException(int contentId) : base($"Объявление с таким ID [{contentId}] не было найдено.")
        {
        }
    }
}