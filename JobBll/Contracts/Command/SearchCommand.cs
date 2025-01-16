
using System.ComponentModel.DataAnnotations;

namespace JobBll.Contracts.Command;

/// <summary>
/// Команда для поиска
/// </summary>
public sealed class SearchCommand
{
    /// <summary>
    /// Строка для поиска
    /// </summary>
    public string SearchText { get; set; } = null!;

    /// <summary>
    /// Город. Пока не используется
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Минимальная оплата
    /// </summary>
    [Range(1, long.MaxValue)]
    public long? MinSalary { get; set; }

    /// <summary>
    /// Максимальная оплата
    /// </summary>
    [Range(1, long.MaxValue)]
    public long? MaxSalary { get; set; }
}
