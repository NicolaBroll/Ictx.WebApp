namespace Ictx.WebApp.Api.Common;

public interface IAuthSettings
{
    string Audience { get; set; }
    string Authority { get; set; }
}

public class AuthSettings : IAuthSettings
{
    public string Authority { get; set; }
    public string Audience { get; set; }
    public char[] Key { get; internal set; }
}