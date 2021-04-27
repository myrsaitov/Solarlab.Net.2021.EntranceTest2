using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Mail.Interfaces;

namespace SL2021.Infrastructure.Mail
{
    public class MailServiceMock : IMailService
    {
        public Task Send(string recipient, string subject, string message, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
