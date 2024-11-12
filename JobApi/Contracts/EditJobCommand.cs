namespace JobApi.Contracts;

public sealed class EditJobCommand : AddJobCommand
{
    /// <summary>
    /// Идентификатор вакансии
    /// </summary>
    public long Id { get; set; }
}
