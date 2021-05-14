using System;

namespace Ictx.WebApp.Core.Entities.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}";
        }
    }
}
