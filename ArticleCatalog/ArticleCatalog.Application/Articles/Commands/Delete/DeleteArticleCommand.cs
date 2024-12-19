using MediatR;

public class DeleteArticleCommand : EntityCommand, IRequest<Result>
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Result>
    {
        private readonly IArticleDomainRepository articleRepository;

        public DeleteArticleCommandHandler(IArticleDomainRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public async Task<Result> Handle(
            DeleteArticleCommand request, 
            CancellationToken cancellationToken)
        {
            await articleRepository.Delete(
                request.Id,
                cancellationToken);

            return Result.Success;
        }
    }
}