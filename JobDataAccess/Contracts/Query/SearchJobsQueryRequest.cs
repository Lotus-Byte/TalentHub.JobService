
namespace JobDataAccess.Contracts.Query;

public sealed class SearchJobsQueryRequest
{
    public string SearchText { get; set; } = null!;

    public string? City { get; set; }

    public long? MinSalary { get; set; }

    public long? MaxSalary { get; set; }
}
