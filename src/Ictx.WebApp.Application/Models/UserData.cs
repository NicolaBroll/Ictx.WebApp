namespace Ictx.WebApp.Application.Models
{
    public interface IUserData
    {
        int UserId { get; }
    }

    public class UserData : IUserData
    {
        public int UserId { get; private set; }

        public UserData(int userId)
        {
            this.UserId = userId;
        }
    }
}
