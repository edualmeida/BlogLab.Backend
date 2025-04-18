using ArticleCatalog.Domain.Builders;
using Common.Application.Contracts;
using MediatR;
using ArticleCatalog.Domain.Models.Articles;

namespace ArticleCatalog.Application.Articles.Commands.Create;
public sealed class BuildArticleDomain : IRequest<Article>
{
    public required CreateArticleCommand Command { get; set; }

    public sealed class BuildArticleDomainHandler(
        ICurrentUserService currentUserService,
        IArticleBuilder articleBuilder) : IRequestHandler<BuildArticleDomain, Article>
    {
        public Task<Article> Handle(
            BuildArticleDomain request,
            CancellationToken cancellationToken)
        {

            var article = articleBuilder
                .WithTitle(request.Command.Title)
                .WithSubtitle(request.Command.Subtitle)
                .WithText(request.Command.Text)
                .WithCategoryId(request.Command.CategoryId)
                .WithThumbnailId(request.Command.ThumbnailId)
                .WithAuthorId(currentUserService.GetRequiredUserId())
                .Build();

            return Task.FromResult(article);
        }
    }
}
