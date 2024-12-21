using MediatR;

public class DeleteArticleCommand : EntityCommand, IRequest<Result>
{
    public class DeleteArticleCommandHandler(IArticleDomainRepository articleRepository) 
        : IRequestHandler<DeleteArticleCommand, Result>
    {
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