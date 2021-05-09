using System.Threading.Tasks;
using TransactionalEmail.Core.ValueObjects;

namespace TransactionalEmail.Core.Interfaces.Services
{
    public interface IEmailQueueService
    {
        ValueTask Enqueue(EmailValueObject emailValueObject);
    }
}
