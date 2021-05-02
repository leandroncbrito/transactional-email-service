using System.Threading.Tasks;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Core.Interfaces
{
    public interface IMailProvider
    {
        Task<bool> SendEmailAsync(EmailDTO emailDTO);
    }
}
