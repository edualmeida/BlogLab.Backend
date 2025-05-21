using ArticleCatalog.Application.Comments.Queries.GetById;
using Comments.Application.Comments.Exceptions;
using Comments.Application.Comments.Queries.GetPaginated;
using Comments.Domain.Models.Comments;
using Comments.Domain.Repositories;
using Common.Application;
using MediatR;

namespace Comments.Application.Comments.Commands.Create;
internal sealed class CreateElasticComment(Comment comment) : IRequest<Result>
{
    public Comment Comment => comment;

    public class CreateElasticArticleHandler(
        IMediator mediator,
        IElasticCommentRepository repository) : IRequestHandler<CreateElasticComment, Result>
    {
        public async Task<Result> Handle(CreateElasticComment request, CancellationToken cancellationToken)
        {
            var articleQueryResponse = await mediator
                .Send(new GetCommentByIdQuery() { Id = request.Comment.Id }, cancellationToken);

            var response = await repository.CreateCommentAsync(new ElasticComment()
            {
                Id = request.Comment.Id,
                Text = request.Comment.Text,
                ArticleId = request.Comment.ArticleId,
                AuthorId = articleQueryResponse.AuthorId,
                Author = articleQueryResponse.Author,
                CreatedOnUtc = request.Comment.CreatedOnUtc,
            });

            if (!response)
            {
                return Result.Failure(new ElasticArticleNotCreatedException(request.Comment.Id).Message);
            }

            return Result.Success;
        }
    }
}
