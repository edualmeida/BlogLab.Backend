using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class IdentityController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet]
    [Authorize(AuthenticationSchemes = ApiKey.SchemeName)]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
        => await Send(new UserGetAllQuery());

    [HttpGet]
    [Route(Id)]
    [Authorize(AuthenticationSchemes = ApiKey.SchemeName)]
    public async Task<ActionResult<UserResponse>> GetById([FromRoute] UserGetByIdQuery query)
        => await Send(query);

    [HttpPost]
    [Authorize(AuthenticationSchemes = ApiKey.SchemeName)]
    [Route(nameof(Register))]
    public async Task<ActionResult> Register(
        RegisterUserCommand command)
        => await Send(command);

    [HttpPost]
    [Route(nameof(Login))]
    public async Task<ActionResult<UserResponseModel>> Login(
        LoginUserCommand command)
        => await Send(command);

    [HttpPut]
    [Authorize]
    [Route(nameof(ChangePassword))]
    public async Task<ActionResult> ChangePassword(
        ChangePasswordCommand command)
        => await Send(command);
}