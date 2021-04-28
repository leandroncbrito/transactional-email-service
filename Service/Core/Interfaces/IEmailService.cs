
using System.Threading.Tasks;

namespace Service.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string message);
    }
}
