namespace Comments.Application.Comments.Commands.Create;
public class CreateCommentResponse
{
    internal CreateCommentResponse(Guid id) => Id = id;

    public Guid Id { get; }
}