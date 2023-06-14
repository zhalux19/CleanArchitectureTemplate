using KafkaTemplate.Api.Models;
using KafkaTemplate.Core.Commands.Accounts.CreateAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace KafkaTemplate.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountBody createAccountBody, CancellationToken cancellationToken)
        {
            var createAccountCommand = new CreateAccountCommand() { Name = createAccountBody.Name, IsAdmin = false };
            var result = await _mediator.Send(createAccountCommand, cancellationToken);
            return result.Match(
                _ => Ok(),
                errors => ValidationProblem());


        }
    }
}
