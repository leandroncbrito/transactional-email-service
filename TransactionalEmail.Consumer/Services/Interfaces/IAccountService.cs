using System.Threading.Tasks;
using TransactionalEmail.Consumer.Requests;
using TransactionalEmail.Consumer.Responses;
using TransactionalEmail.Consumer.ValueObjects;

namespace TransactionalEmail.Consumer.Services
{
    public interface IAccountService
    {
        Task<HttpClientResponse> RegisterAsync(Register register);

        Task<HttpClientResponse> ForgotPasswordAsync(ForgotPassword forgotPassword);

        Task<HttpClientResponse> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
