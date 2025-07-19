using ArticleCatalog.Application.Contracts.Authors;
using ArticleCatalog.Application.Services;
using ArticleCatalog.Domain.Telemetry;
using MediatR;

namespace ArticleCatalog.Application.Authors.Queries;
public class GetAuthorsQuery: IRequest<List<AuthorResponse>>
{
    public class GetAuthorsQueryHandler(
        IAuthorsHttpService authorsHttpService,
        IArticleCatalogMetrics authorsMetrics) : IRequestHandler<GetAuthorsQuery, List<AuthorResponse>>
    {
        public async Task<List<AuthorResponse>> Handle(
            GetAuthorsQuery request,
            CancellationToken cancellationToken)
         {
            authorsMetrics.IncrementApiCallCount();

            var result = await authorsMetrics
                .MeasureAuthorsApiCallAsync(() => authorsHttpService.GetAll(cancellationToken));

            return result ?? [];
        }
    }
}
