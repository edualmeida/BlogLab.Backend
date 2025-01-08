public class CreateUserResponse
{
    internal CreateUserResponse(Guid id) => Id = id;

    public Guid Id { get; }
}