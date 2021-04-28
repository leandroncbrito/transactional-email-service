using System.Threading.Tasks;

namespace Service.Core.Interfaces
{
    public interface ISendGridProvider
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
