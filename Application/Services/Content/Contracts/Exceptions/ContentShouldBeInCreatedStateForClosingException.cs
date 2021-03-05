using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Content.Contracts.Exceptions
{
    public sealed class ContentShouldBeInCreatedStateForClosingException : EntityNotInValidStateException
    {
        public ContentShouldBeInCreatedStateForClosingException(int adId) 
            : base($"Объявление с ID [{adId}] должно быть в статусе {Domain.Content.Statuses.Created} для закрытия")
        {
        }
    }
}