using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(AuthenticationSchemes = ApiKey.SchemeName)]
public class UsersController(IMediator mediator) : ApiController(mediator)
{
    [HttpPost]
    public async Task<ActionResult<CreateUserResponse>> Create(CreateUserCommand command)
        => await Send(command);
}