using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TransactionalEmail.Consumer.Requests;
using TransactionalEmail.Consumer.Responses;

namespace TransactionalEmail.Consumer.Services
{
    public interface IAccountService
    {
        Task<HttpClientResponse> RegisterAsync(RegisterRequest request);

        Task<HttpClientResponse> ForgotPasswordAsync(ForgotPasswordRequest request);

        Task<HttpClientResponse> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
