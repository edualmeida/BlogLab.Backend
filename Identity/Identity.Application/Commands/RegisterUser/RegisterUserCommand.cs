using MediatR;

public class RegisterUserCommand : UserRequestModel, IRequest<Result>
{
    public RegisterUserCommand(
        string email,
        string password,
        string confirmPassword,
        string firstName,
        string middleName,
        string surName,
        RegisterUserRole role)
        : base(email, password)
    {
        ConfirmPassword = confirmPassword;
        FirstName = firstName;
        MiddleName = middleName;
        Surname = surName;
        Role = role;
    }

    public string ConfirmPassword { get; }
    public string FirstName { get; private set; }
    public string MiddleName { get; private set; }
    public string Surname { get; private set; }
    public RegisterUserRole Role { get; set; }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
    {
        private readonly IIdentity identity;

        public RegisterUserCommandHandler(IIdentity identity)
            => this.identity = identity;

        public async Task<Result> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
            => await this.identity.Register(request);
    }
}