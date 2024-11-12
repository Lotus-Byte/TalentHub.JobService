namespace JobApi.Contracts;

public sealed class SearchCommand
{
    public string SearchText { get; set; } = null!;

    public string? City { get; set; }

    public long? SalaryLevel { get; set; }
}
