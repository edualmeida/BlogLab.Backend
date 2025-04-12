using Identity.Application.Queries.Common;
using MediatR;

namespace Identity.Application.Queries.GetAll;
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