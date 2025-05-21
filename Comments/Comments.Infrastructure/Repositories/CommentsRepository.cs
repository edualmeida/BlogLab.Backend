using Comments.Application.Comments.Queries;
using Comments.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Comments.Domain.Models.Comments;
using AutoMapper;
using Comments.Application.Comments.Queries.Common;
using Comments.Infrastructure.Repositories.Configuration;

namespace Comments.Infrastructure.Repositories;
internal sealed class CommentsRepository :
    ICommentDomainRepository, ICommentsQueryRepository
{
    private readonly IMongoCollection<Comment> commentsCollection;
    private readonly IMapper mapper;

    public CommentsRepository(
        IMapper mapper,
        IOptions<MongoStoreDatabaseSettings> settings)
    {
        this.mapper = mapper;
        var mongoClient = new MongoClient(
            settings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            settings.Value.DatabaseName);

        commentsCollection = mongoDatabase.GetCollection<Comment>(
            settings.Value.CollectionName);
    }

    public async Task<List<CommentQueryResponse>> GetAll(Guid articleId) =>
        mapper.Map<List<CommentQueryResponse>>(await commentsCollection.Find(_ => true).ToListAsync());

    public async Task<CommentQueryResponse?> GetById(Guid id) =>
        mapper.Map<CommentQueryResponse?>(await commentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync());

    public async Task<Comment?> Find(Guid id) =>
        await commentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Comment newItem) =>
        await commentsCollection.InsertOneAsync(newItem);

    public async Task UpdateAsync(Guid id, Comment updateItem) =>
        await commentsCollection.ReplaceOneAsync(x => x.Id == id, updateItem);

    public async Task RemoveAsync(Guid id) =>
        await commentsCollection.DeleteOneAsync(x => x.Id == id);

    public Task<Guid> Save(Comment entity, CancellationToken cancellationToken = default)
    {
        return CreateAsync(entity)
            .ContinueWith(_ => entity.Id, cancellationToken);
    }
}