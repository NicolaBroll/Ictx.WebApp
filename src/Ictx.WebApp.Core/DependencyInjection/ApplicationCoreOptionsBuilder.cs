using Ictx.WebApp.Core.Common;

namespace Ictx.WebApp.Core.DependencyInjection
{
    public class ApplicationCoreOptionsBuilder :
            IApplicationDatacontextConfigurationStage,
            IMailConfigurationStage
    {
        private string _connectionSting;
        private MailSettings _mailSettings;

        private ApplicationCoreOptionsBuilder()
        { }

        public static IApplicationDatacontextConfigurationStage Configure()
        {
            return new ApplicationCoreOptionsBuilder();
        }

        public IMailConfigurationStage ApplicationDatacontextConfigurationStage(string connectionSting)
        {
            this._connectionSting = connectionSting;
            return this;
        }

        public ApplicationCoreOptions MailConfigurationStage(MailSettings mailSettings)
        {
            this._mailSettings = mailSettings;
            return new ApplicationCoreOptions(this._connectionSting, this._mailSettings);
        }


    }

    #region Interface

    public interface IApplicationDatacontextConfigurationStage
    {
        public IMailConfigurationStage ApplicationDatacontextConfigurationStage(string connectionSting);
    }

    public interface IMailConfigurationStage
    {
        public ApplicationCoreOptions MailConfigurationStage(MailSettings mailSettings);
    }

    #endregion
}
