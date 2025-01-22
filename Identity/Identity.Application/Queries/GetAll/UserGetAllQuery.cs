using MediatR;

public class UserGetAllQuery : IRequest<List<UserResponse>>
{
    public class UserGetAllQueryHandler(
        IIdentityQueryRepository repository
        ) : IRequestHandler<UserGetAllQuery, List<UserResponse>>
    {
        public async Task<List<UserResponse>> Handle(
            UserGetAllQuery request, 
            CancellationToken cancellationToken)
            => await repository.GetAll();
    }
}