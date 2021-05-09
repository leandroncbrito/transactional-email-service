using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Consumer.Helpers;
using TransactionalEmail.Consumer.Requests;
using TransactionalEmail.Consumer.Responses;
using TransactionalEmail.Consumer.Services;
using TransactionalEmail.Consumer.ValueObjects;

namespace TransactionalEmail.Consumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly ILogger<AccountController> logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            this.accountService = accountService;
            this.logger = logger;
        }

        [HttpPost("register", Name = "register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                logger.LogInformation("Registering new user endpoint");

                var register = new Register(request.Email);

                var response = await accountService.RegisterAsync(register);

                logger.LogInformation("Register email sent successfully");

                return Accepted(response);
            }
            catch (HttpRequestException ex)
            {
                logger.LogError($"It was not possible to send the register email to {request.Email}");
                logger.LogCritical(ex.Message, ex);

                var error = new HttpClientResponse(ex.StatusCode, ex.Message);
                return BadRequest(error);
            }
        }

        [HttpPost("forgot-password", Name = "forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                var token = TokenHelper.Generate();

                var validateUrl = UrlHelper.Generate(HttpContext.Request, Url.RouteUrl("validate-reset-token"), $"token={token}");

                logger.LogInformation("Forgot password endpoint");

                var forgotPasswordDTO = new ForgotPassword(request.Email, validateUrl);

                var response = await accountService.ForgotPasswordAsync(forgotPasswordDTO);

                logger.LogInformation("Forgot password email sent successfully");

                return Ok(response);
            }
            catch (HttpRequestException ex)
            {
                logger.LogError($"It was not possible to send the forgot password email to {request.Email}");
                logger.LogCritical(ex.Message, ex);

                var error = new HttpClientResponse(ex.StatusCode, ex.Message);
                return BadRequest(error);
            }
        }

        [HttpGet("validate-reset-token", Name = "validate-reset-token")]
        public IActionResult ValidateResetToken([FromQuery] string token)
        {
            var response = new HttpClientResponse(HttpStatusCode.OK, "Token is valid");

            return Ok(response);
        }

        [HttpPost("reset-password", Name = "reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                //Test if User.ResetToken === request.Token
                var response = accountService.ResetPasswordAsync(request);

                // if (!response.Success)
                // {
                //     //var errors = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => x.ErrorMessage));
                //     return BadRequest(response);
                // }

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message, ex);

                var error = new HttpClientResponse(HttpStatusCode.InternalServerError, ex.Message);
                return BadRequest(error);
            }
        }
    }
}
