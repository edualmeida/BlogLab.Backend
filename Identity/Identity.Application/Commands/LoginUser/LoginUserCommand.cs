using MediatR;
namespace Identity.Application.Commands.LoginUser;
public class LoginUserCommand : UserRequestModel, IRequest<Result<LoginResponseModel>>
{
    public LoginUserCommand(string email, string password)
        : base(email, password)
    {
    }

    public class LoginUserCommandHandler(IIdentity identity) : IRequestHandler<LoginUserCommand, Result<LoginResponseModel>>
    {
        public async Task<Result<LoginResponseModel>> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
            => await identity.Login(request);
    }
}