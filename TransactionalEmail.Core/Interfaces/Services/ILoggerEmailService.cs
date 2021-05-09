using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Core.Interfaces.Services
{
    public interface IEmailLoggerService
    {
        void Store(EmailDTO email);
    }
}
