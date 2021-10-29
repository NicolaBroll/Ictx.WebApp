using Ictx.WebApp.Infrastructure.Common;

namespace Ictx.WebApp.Infrastructure.DependencyInjection
{
    public class InfrastructureOptionsBuilder :
            IApplicationDatacontextConfigurationStage,
            IMailConfigurationStage
    {
        private string _connectionSting;
        private MailSettings _mailSettings;

        private InfrastructureOptionsBuilder()
        { }

        public static IApplicationDatacontextConfigurationStage Configure()
        {
            return new InfrastructureOptionsBuilder();
        }

        public IMailConfigurationStage ApplicationDatacontextConfigurationStage(string connectionSting)
        {
            this._connectionSting = connectionSting;
            return this;
        }

        public InfrastructureOptions MailConfigurationStage(MailSettings mailSettings)
        {
            this._mailSettings = mailSettings;
            return new InfrastructureOptions(this._connectionSting, this._mailSettings);
        }


    }

    #region Interface

    public interface IApplicationDatacontextConfigurationStage
    {
        public IMailConfigurationStage ApplicationDatacontextConfigurationStage(string connectionSting);
    }

    public interface IMailConfigurationStage
    {
        public InfrastructureOptions MailConfigurationStage(MailSettings mailSettings);
    }

    #endregion
}
