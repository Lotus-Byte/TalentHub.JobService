
namespace JobBll.Contracts.Internal;

internal sealed class BusConfirmationMessage
{
    public required string JobName { get; init; }
    
    public required Guid CreateUserId { get; init; }

    public required DateTimeOffset Timestamp { get; init; }
}
