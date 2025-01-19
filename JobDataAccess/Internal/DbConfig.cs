namespace JobDataAccess.Internal;

internal static class DbConfig
{
    public const string SCHEMA_NAME = "job";

    public const string CONNECTION_STRING_KEY = "JobDb";

    // пока вынесем в константу
    public const string FT_SEARCH_CONFIG = "russian";
}
