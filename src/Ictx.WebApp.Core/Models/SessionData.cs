namespace Ictx.WebApp.Core.Models
{
    public interface ISessionData
    {
        int UserId { get; }
    }

    public class SessionData : ISessionData
    {
        public int UserId { get; private set; }

        public SessionData(int userId)
        {
            this.UserId = userId;
        }
    }
}
