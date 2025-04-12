using Common.Application;
using Common.Application.Contracts;
using MediatR;

namespace Identity.Application.Commands.ChangePassword;
public class ChangePasswordCommand : IRequest<Result>
{
    public string CurrentPassword { get; set; } = default!;

    public string NewPassword { get; set; } = default!;

    public class ChangePasswordCommandHandler(
            IIdentity identity,
            ICurrentUserService currentUser) : 
        IRequestHandler<ChangePasswordCommand, Result>
    {
        public async Task<Result> Handle(
            ChangePasswordCommand request,
            CancellationToken cancellationToken)
            => await identity.ChangePassword(new ChangePasswordRequestModel(
                currentUser.GetRequiredUserId(),
                request.CurrentPassword,
                request.NewPassword));
    }
}