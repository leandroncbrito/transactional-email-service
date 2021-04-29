using System.Threading.Tasks;

namespace Service.Core.Interfaces
{
    public interface IMailProvider
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
