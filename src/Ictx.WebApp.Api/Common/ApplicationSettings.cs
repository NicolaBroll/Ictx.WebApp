namespace Ictx.WebApp.Api.Common
{
    public interface IApplicationSettings 
    {
        public int UploadMaxSize { get; set; }

    }
    public class ApplicationSettings: IApplicationSettings
    {
        public int UploadMaxSize { get; set; }
    }
}
