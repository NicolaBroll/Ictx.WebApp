using System;

namespace Ictx.WebApp.Fwk.Entities.Base
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertedUtc { get; set; }
        public DateTime UpdatedUtc { get; set; }
        public DateTime? DeletedUtc { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}";
        }
    }
}
