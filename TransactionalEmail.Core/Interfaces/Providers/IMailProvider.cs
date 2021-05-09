using System.Threading.Tasks;
using TransactionalEmail.Core.ValueObjects;

namespace TransactionalEmail.Core.Interfaces.Providers
{
    public interface IMailProvider
    {
        Task<bool> SendEmailAsync(EmailValueObject emailValueObject);
    }
}
