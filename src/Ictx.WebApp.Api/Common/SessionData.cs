namespace Ictx.WebApp.Api.Common
{
    public class SessionData
    {
        public int UserId { get; private set; }

        public SessionData(int userId)
        {
            this.UserId = userId;
        }
    }
}
