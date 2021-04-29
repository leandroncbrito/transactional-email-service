using System.Threading.Tasks;

namespace TransactionalEmail.Core.Interfaces
{
    public interface IMailProvider
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
