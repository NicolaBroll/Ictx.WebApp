using Ictx.WebApp.Core.Entities.Base;

namespace Ictx.WebApp.Core.Entities
{
    public class Operation : BaseEntityUser
    {
        public bool Started { get; set; }
        public bool Completed { get; set; }
        public string Data { get; set; }
    }
}
