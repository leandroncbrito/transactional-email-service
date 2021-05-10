using System.Threading.Tasks;
using TransactionalEmail.Consumer.DTO;
using TransactionalEmail.Consumer.Requests;
using TransactionalEmail.Consumer.Responses;

namespace TransactionalEmail.Consumer.Interfaces.Services
{
    public interface IAccountService
    {
        Task<HttpClientResponse> RegisterAsync(RegisterDTO register);

        Task<HttpClientResponse> ForgotPasswordAsync(ForgotPasswordDTO forgotPassword);

        Task<HttpClientResponse> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
