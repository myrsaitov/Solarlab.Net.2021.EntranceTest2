using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Content.Contracts.Exceptions
{
    public sealed class NoAdFoundException : NotFoundException
    {
        public NoAdFoundException(int contentId) : base($"Объявление с таким ID [{contentId}] не было найдено.")
        {
        }
    }
}