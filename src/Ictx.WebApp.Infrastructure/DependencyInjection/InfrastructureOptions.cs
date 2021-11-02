using Ictx.WebApp.Infrastructure.Common;

namespace Ictx.WebApp.Infrastructure.DependencyInjection
{
    public class InfrastructureOptions
    {
        public string ConnectionString { get; }
        public MailSettings MailSettings { get; }

        public InfrastructureOptions(string connectionString, MailSettings mailSettings)
        {
            ConnectionString = connectionString;
            MailSettings = mailSettings;
        }
    }
}
