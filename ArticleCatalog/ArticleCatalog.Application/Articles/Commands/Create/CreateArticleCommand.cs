using ArticleCatalog.Application.Articles.Commands.Common;
using ArticleCatalog.Domain.Builders;
using ArticleCatalog.Domain.Repositories;
using Common.Application;
using Common.Application.Contracts;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Create;
public class CreateArticleCommand : ArticleCommand, IRequest<Result<CreateArticleResponse>>
{
    public class CreateArticleCommandHandler(
        ICurrentUserService currentUserService,
        IArticleDomainRepository articleRepository,
        IArticleBuilder articleBuilder) : IRequestHandler<CreateArticleCommand, Result<CreateArticleResponse>>
    {
        public async Task<Result<CreateArticleResponse>> Handle(
            CreateArticleCommand request,
            CancellationToken cancellationToken)
        {
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
    }
}