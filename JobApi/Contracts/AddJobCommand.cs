namespace JobApi.Contracts;

public class AddJobCommand
{
    /// <summary>
    /// Наименование вакансии
    /// </summary>
    public string Name { get; set; } = null!;


    /// <summary>
    /// Город вакансии
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Уровень зарплаты
    /// </summary>
    public int? SalaryLevel { get; set; }

    /// <summary>
    /// Описание вакансии
    /// </summary>
    public string Description { get; set; } = null!;
}
