using System.Threading.Tasks;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Core.Interfaces.Services
{
    public interface IEmailQueueService
    {
        ValueTask Enqueue(EmailDTO email);
    }
}
