using Identity.Application.Queries.Common;
using MediatR;

namespace Identity.Application.Queries.GetById;
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