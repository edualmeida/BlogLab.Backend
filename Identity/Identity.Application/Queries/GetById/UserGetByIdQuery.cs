using MediatR;

public class UserGetByIdQuery : EntityCommand, IRequest<UserResponse>
{
    public class UserGetByIdQueryHandler(IIdentityQueryRepository repository) 
        : IRequestHandler<UserGetByIdQuery, UserResponse>
    {
        public async Task<UserResponse> Handle(
            UserGetByIdQuery request,
            CancellationToken cancellationToken)
            => await repository.GetById(
                request.Id,
                cancellationToken);
    }
}