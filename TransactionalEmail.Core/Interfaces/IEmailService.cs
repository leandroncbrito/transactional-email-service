
using System.Threading.Tasks;

namespace TransactionalEmail.Core.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string message);
    }
}
