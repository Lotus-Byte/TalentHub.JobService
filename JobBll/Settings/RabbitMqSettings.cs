
namespace JobBll.Settings;

public sealed class RabbitMqSettings
{
    public required string Host { get; set; }

    public required string VHost { get; set; }

    public required string Login { get; set; }

    public required string Password { get; set; }
}
