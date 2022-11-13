using System.Reflection;


namespace EF
{
    public static class Config
    {
        public static string? EXE_PATH => AppDomain.CurrentDomain.BaseDirectory;
        

        static Version? Version { get; } = Assembly.GetExecutingAssembly()?.GetName()?.Version;

        static DateTime? BuildDate
        {
            get
            {
                if (Version != null)
                    return new DateTime(2000, 1, 1).AddDays(Version.Build).AddSeconds(Version.Revision * 2);
                else
                    return null;
            }
        }
        public static string DisplayableVersion => $"{Version} ({BuildDate})";

        static Config()
        {
            
        }


    }


}