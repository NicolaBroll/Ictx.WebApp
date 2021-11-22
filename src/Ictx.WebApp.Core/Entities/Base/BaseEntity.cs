using System;

namespace Ictx.WebApp.Core.Entities.Base
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime InsertedUtc { get; set; }
        public DateTime UpdatedUtc { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}";
        }
    }
}
