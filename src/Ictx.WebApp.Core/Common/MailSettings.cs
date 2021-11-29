namespace Ictx.WebApp.Core.Common
{
    public interface IMailSettings
    {
        public string FromMailAddress { get; set; }
        public string FromName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
    }

    public class MailSettings: IMailSettings
    {
        public string FromMailAddress { get; set; }
        public string FromName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
    }
}
