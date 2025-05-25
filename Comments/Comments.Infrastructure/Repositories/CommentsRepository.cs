using AutoMapper;
using Comments.Application.Comments.Queries;
using Comments.Application.Comments.Queries.Common;
using Comments.Domain.Models.Comments;
using Comments.Domain.Repositories;
using Comments.Infrastructure.Repositories.Configuration;
using Common.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

namespace Comments.Infrastructure.Repositories;
internal sealed class CommentsRepository : MongoRepository<Comment>,
    ICommentDomainRepository, ICommentsQueryRepository
{
    private readonly IMapper _mapper;

    public CommentsRepository(
        IMapper mapper,
        IOptions<CommentsMongoDatabaseOptions> settings) : base(settings.Value)
    {
        _mapper = mapper;
    }

    public async Task<Comment?> Find(Guid id) =>
        await GetByIdAsync(id);

    public async Task<List<CommentQueryResponse>> GetAll(Guid articleId)
    {
        var comments = await FindAsync(x => x.ArticleId == articleId);

        return _mapper.Map<List<CommentQueryResponse>>(comments);
    }

    public async Task<CommentQueryResponse?> GetById(Guid id)
    {
        return _mapper.Map<CommentQueryResponse?>(await GetByIdAsync(id));
    }

    public Task<Guid> Save(Comment entity, CancellationToken cancellationToken = default)
    {
        return CreateAsync(entity)
            .ContinueWith(_ => entity.Id, cancellationToken);
    }
}