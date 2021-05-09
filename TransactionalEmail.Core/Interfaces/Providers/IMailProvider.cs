using System.Threading.Tasks;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Core.Interfaces.Providers
{
    public interface IMailProvider
    {
        Task<bool> SendEmailAsync(EmailDTO emailDTO);
    }
}
