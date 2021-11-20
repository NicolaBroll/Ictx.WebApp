namespace Ictx.WebApp.Core.Models
{
    public class UserData
    {
        public int UserId { get; private set; }

        public UserData(int userId)
        {
            this.UserId = userId;
        }
    }
}
