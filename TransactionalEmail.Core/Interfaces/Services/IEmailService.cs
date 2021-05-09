
using System.Threading.Tasks;
using TransactionalEmail.Core.ValueObjects;

namespace TransactionalEmail.Core.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailValueObject emailValueObject);
    }
}
