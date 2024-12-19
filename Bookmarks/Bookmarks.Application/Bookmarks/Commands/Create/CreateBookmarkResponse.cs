public class CreateBookmarkResponse
{
    public CreateBookmarkResponse(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Guid CustomerId { get; }
}