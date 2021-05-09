using TransactionalEmail.Core.ValueObjects;

namespace TransactionalEmail.Core.Interfaces.Services
{
    public interface IEmailLoggerService
    {
        void Store(EmailValueObject emailValueObject);
    }
}
