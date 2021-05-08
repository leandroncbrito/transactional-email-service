using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Consumer.Requests;
using TransactionalEmail.Consumer.Responses;
using TransactionalEmail.Consumer.Services;

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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                logger.LogInformation("Registering new user endpoint");

                var response = await accountService.RegisterAsync(request);

                if (!response.Success)
                {
                    //var errors = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => x.ErrorMessage));
                    logger.LogInformation("It was not possible to send the register email", response.Title);
                    return BadRequest(response);
                }

                logger.LogInformation("Register email sent successfully");
                return Ok(response);
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex.Message, ex);
                var error = new HttpClientResponse(ex.StatusCode, ex.Message);
                return BadRequest(error);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                logger.LogInformation("Forgot password endpoint");

                var response = await accountService.ForgotPasswordAsync(request);

                if (!response.Success)
                {
                    //var errors = ModelState.Keys.SelectMany(key => ModelState[key].Errors.Select(x => x.ErrorMessage));
                    logger.LogInformation("It was not possible to send the forgot password email", response.Title);
                    return BadRequest(response);
                }

                logger.LogInformation("Forgot password email sent successfully");
                return Ok(response);
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex.Message, ex);
                var error = new HttpClientResponse(ex.StatusCode, ex.Message);
                return BadRequest(error);
            }
        }

        [HttpGet("validate-reset-token")]
        public IActionResult ValidateResetToken([FromQuery] string token)
        {
            var response = new HttpClientResponse(HttpStatusCode.OK, "Token is valid", true);

            return Ok(response);
        }

        [HttpPost("reset-password")]
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
            catch (HttpRequestException ex)
            {
                logger.LogError(ex.Message, ex);
                var error = new HttpClientResponse(ex.StatusCode, ex.Message);
                return BadRequest(error);
            }
        }
    }
}
