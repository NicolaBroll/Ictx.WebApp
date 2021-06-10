using Ictx.WebApp.Core.Entities.Base;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Core.Entities
{
    public class Operation : BaseEntityUser
    {   
        public BackgroundOperationType Tipo { get; set; }
        public string Data { get; set; }
        public bool Started { get; set; }
        public bool Completed { get; set; }
    }
}
