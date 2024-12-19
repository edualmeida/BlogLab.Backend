using MediatR;

public class ArticleGetByIdQuery : EntityCommand, IRequest<ArticleResponse>
{
    public class ArticleDetailsQueryHandler : IRequestHandler<ArticleGetByIdQuery, ArticleResponse>
    {
        private readonly IArticleQueryRepository articleRepository;

        public ArticleDetailsQueryHandler(IArticleQueryRepository articleRepository)
            => this.articleRepository = articleRepository;

        public async Task<ArticleResponse> Handle(
            ArticleGetByIdQuery request,
            CancellationToken cancellationToken)
            => await articleRepository.GetById(
                request.Id,
                cancellationToken);
    }
}