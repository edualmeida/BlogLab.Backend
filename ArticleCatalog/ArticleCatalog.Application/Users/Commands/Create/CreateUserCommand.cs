using MediatR;

public class CreateUserCommand : UserCommand, IRequest<CreateUserResponse>
{
    public class CreateUserCommandHandler(
        IUserDomainRepository repository,
        IUserBuilder builder) : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        public async Task<CreateUserResponse> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = builder
                .WithFirstName(request.FirstName)
                .WithMiddleName(request.MiddleName)
                .WithSurname(request.Surname)
                .WithEmail(request.Email)
                .WithPassword(request.Password)
                .Build();

            await repository.Save(user, cancellationToken);

            return new CreateUserResponse(user.Id);
        }
    }
}