namespace JobBll.Contracts.Dto;

public sealed record CreateJobDto
{
    public long Id { get; init; }

    public Guid ObjectId { get; init; }
}
