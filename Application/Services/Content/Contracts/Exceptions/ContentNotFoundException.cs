using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Services.Content.Contracts.Exceptions
{
    public sealed class ContentNotFoundException : NotFoundException
    {
        private const string MessageTemplate = "Поздравление с таким ID[{0}] не было найдено.";
        public ContentNotFoundException(int contentId) : base(string.Format(MessageTemplate, contentId))
        {
        }
    }
}