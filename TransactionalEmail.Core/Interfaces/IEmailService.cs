
using System.Threading.Tasks;

namespace TransactionalEmail.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string message);
    }
}
