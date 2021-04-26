using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Mail.Interfaces;

namespace WidePictBoard.Infrastructure.Mail
{
    public class MailServiceMock : IMailService
    {
        public Task Send(string recipient, string subject, string message, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
