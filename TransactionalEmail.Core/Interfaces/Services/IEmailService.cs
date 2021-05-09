
using System.Threading.Tasks;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Core.Interfaces.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailDTO emailDTO);
    }
}
