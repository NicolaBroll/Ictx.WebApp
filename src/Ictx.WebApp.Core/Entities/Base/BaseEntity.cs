namespace Ictx.WebApp.Core.Entities.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}";
        }
    }
}
