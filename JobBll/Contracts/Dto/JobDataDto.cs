namespace JobBll.Contracts.Dto;

public sealed record JobDataDto
{
    public long Id { get; init; }

    public Guid ObjectId { get; init; }

    public string Name { get; init; } = null!;

    public long? MinSalary { get; init; }

    public long? MaxSalary { get; init; }

    public string Responsibilities { get; init; } = null!;

    public string Requirements { get; init; } = null!;

    public string Conditions { get; init; } = null!;

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreateStamp { get; init; }
}
