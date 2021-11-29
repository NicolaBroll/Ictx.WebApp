using Ictx.WebApp.Core.Services;

namespace Ictx.WebApp.Core.DependencyInjection
{
    public class ApplicationCoreOptions
    {
        public string ConnectionString { get; }
        public MailSettings MailSettings { get; }

        public ApplicationCoreOptions(string connectionString, MailSettings mailSettings)
        {
            ConnectionString = connectionString;
            MailSettings = mailSettings;
        }
    }
}
