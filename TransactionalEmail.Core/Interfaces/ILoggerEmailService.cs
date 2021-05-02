using System.Threading.Tasks;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Core.Interfaces
{
    public interface IEmailLoggerService
    {
        Task Store(EmailDTO email);
    }
}
