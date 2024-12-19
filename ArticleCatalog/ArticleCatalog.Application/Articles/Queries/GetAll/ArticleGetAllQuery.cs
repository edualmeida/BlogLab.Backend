using MediatR;

public class ArticleGetAllQuery : IRequest<List<ArticleResponse>>
{
    public QueryOrderBy OrderBy { get; set; }
    public class ArticleAllQueryHandler : IRequestHandler<ArticleGetAllQuery, List<ArticleResponse>>
    {
        private readonly IArticleQueryRepository articleRepository;

        public ArticleAllQueryHandler(IArticleQueryRepository articleRepository)
            => this.articleRepository = articleRepository;

        public async Task<List<ArticleResponse>> Handle(ArticleGetAllQuery request, CancellationToken cancellationToken)
            => await articleRepository.GetAll(request.OrderBy, cancellationToken);
    }
}