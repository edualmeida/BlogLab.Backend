﻿using ArticleCatalog.Domain.Builders;
using Common.Application.Contracts;
using MediatR;
using ArticleCatalog.Domain.Models.Articles;

namespace ArticleCatalog.Application.Articles.Commands.Create;
public sealed class BuildArticleDomain(CreateArticleCommand command) : IRequest<Article>
{
    public CreateArticleCommand Command => command;

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
                //.WithThumbnailId(request.Command.ThumbnailId)
                .WithThumbnailId(new Guid("01965f47-83db-7f38-bce5-8c1b8e44ce4a"))
                .WithAuthorId(currentUserService.GetRequiredUserId())
                .Build();

            return Task.FromResult(article);
        }
    }
}
