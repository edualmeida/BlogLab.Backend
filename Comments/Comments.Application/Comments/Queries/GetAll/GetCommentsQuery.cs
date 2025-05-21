using Comments.Application.Comments.Queries.Common;
using Comments.Application.Authors.Queries;
using MediatR;

namespace Comments.Application.Comments.Queries.GetPaginated;
public class GetCommentsQuery : IRequest<GetCommentsResult>
{
    public Guid ArticleId { get; set; }

    public class ArticleAllQueryHandler(IMediator mediator) : 
        IRequestHandler<GetCommentsQuery, GetCommentsResult>
    {
        public async Task<GetCommentsResult> Handle(
            GetCommentsQuery request, 
            CancellationToken cancellationToken)
        {
            var authors = mediator.Send(new GetAuthorsQuery(), cancellationToken);
            var getResult = mediator.Send(new GetRepositoryCommentsQuery
            {
                ArticleId = request.ArticleId,
            }, cancellationToken);

            await Task.WhenAll(getResult, authors);

            getResult.Result.Comments.ForEach(article =>
            {
                article.Author = authors.Result
                    .FirstOrDefault(a => a.Id == article.AuthorId)?
                    .FirstName ?? "ND";
            });

            return getResult.Result;
        }
    }
}