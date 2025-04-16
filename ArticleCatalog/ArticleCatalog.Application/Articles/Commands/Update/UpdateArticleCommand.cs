using ArticleCatalog.Application.Articles.Commands.Common;
using ArticleCatalog.Application.Categories.Queries.GetAll;
using ArticleCatalog.Domain.Repositories;
using Common.Application;
using MediatR;

namespace ArticleCatalog.Application.Articles.Commands.Update;
public class UpdateArticleCommand(Guid id, ArticleCommand articleCommand) 
    : IRequest<Result>
{
    public Guid Id => id;
    public ArticleCommand Article => articleCommand;

    public class UpdateArticleCommandHandler(IMediator mediator, IArticleDomainRepository articleRepository) 
        : IRequestHandler<UpdateArticleCommand, Result>
    {
        public async Task<Result> Handle(
            UpdateArticleCommand updateArticleCommand, 
            CancellationToken cancellationToken)
        {
            var domainArticle = (await articleRepository.Find(updateArticleCommand.Id, cancellationToken))!;

            await VerifyCategoryExists(updateArticleCommand.Article.CategoryId, cancellationToken);

            domainArticle.UpdateTitle(updateArticleCommand.Article.Title);
            domainArticle.UpdateSubtitle(updateArticleCommand.Article.Subtitle);
            domainArticle.UpdateText(updateArticleCommand.Article.Text);
            domainArticle.UpdateCategory(updateArticleCommand.Article.CategoryId);
            domainArticle.UpdateThumbnail(updateArticleCommand.Article.ThumbnailId);

            await articleRepository.Save(domainArticle, cancellationToken);

            return Result.Success;
        }

        private async Task VerifyCategoryExists(Guid categoryId, CancellationToken cancellationToken)
        {
            await mediator.Send(new CategoryGetByIdQuery { Id = categoryId }, cancellationToken);
        }
    }
}