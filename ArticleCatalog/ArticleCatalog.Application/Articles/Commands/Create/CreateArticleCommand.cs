using ArticleCatalog.Application.Articles.Commands.Common;
using ArticleCatalog.Application.Categories.Queries.GetAll;
using ArticleCatalog.Domain.Builders;
using ArticleCatalog.Domain.Repositories;
using Common.Application;
using Common.Application.Contracts;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Create;
public class CreateArticleCommand : ArticleCommand, IRequest<Result<CreateArticleResponse>>
{
    public class CreateArticleCommandHandler(
        IMediator mediator,
        ICurrentUserService currentUserService,
        IArticleDomainRepository articleRepository,
        IArticleBuilder articleBuilder) : IRequestHandler<CreateArticleCommand, Result<CreateArticleResponse>>
    {
        public async Task<Result<CreateArticleResponse>> Handle(
            CreateArticleCommand request,
            CancellationToken cancellationToken)
        {
            await VerifyCategoryExists(request.CategoryId, cancellationToken);

            var article = articleBuilder
                .WithTitle(request.Title)
                .WithSubtitle(request.Subtitle)
                .WithText(request.Text)
                .WithCategoryId(request.CategoryId)
                .WithThumbnailId(request.ThumbnailId)
                .WithAuthorId(currentUserService.GetRequiredUserId())
                .Build();

            await articleRepository.Save(article, cancellationToken);

            return new CreateArticleResponse(article.Id);
        }

        private async Task VerifyCategoryExists(Guid categoryId, CancellationToken cancellationToken)
        {
            await mediator.Send(new CategoryGetByIdQuery { Id = categoryId }, cancellationToken);
        }
    }
}