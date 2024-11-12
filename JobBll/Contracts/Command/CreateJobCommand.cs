namespace JobBll.Contracts.Command;

public sealed class CreateJobCommand
{
    public string Name { get; set; } = null!;

    public long? MinSalary { get; set; }

    public long? MaxSalary { get; set; }

    public string Responsibilities { get; set; } = null!;

    public string Requirements { get; set; } = null!;

    public string Conditions { get; set; } = null!;

    /// <summary>
    /// TODO: стоит мок, так как не понятно как брать UserId
    /// </summary>
    public Guid CreateUserId => Guid.Empty;
}
