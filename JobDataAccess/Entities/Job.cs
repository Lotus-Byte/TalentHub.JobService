using NpgsqlTypes;

namespace JobDataAccess.Entities;

public sealed class Job : IAggregateRoot<long>
{
    public long Id { get; set; }

    public Guid ObjectId { get; set; }

    public string Name { get; set; } = null!;

    public long? MinSalary { get; set; }
    
    public long? MaxSalary { get; set; }

    public string Responsibilities { get; set; } = null!;

    public string Requirements { get; set; } = null!;

    public string Conditions { get; set; } = null!;

    public bool Deleted { get; set; }

    public DateTime Created { get; set; }

    public Guid CreateUserId { get; set; }

    #region FullText support

    public NpgsqlTsVector ResponsibilitiesVector { get; set; } = null!;

    public NpgsqlTsVector RequirementsVector { get; set; } = null!;

    public NpgsqlTsVector ConditionsVector { get; set; } = null!;

    #endregion
}
