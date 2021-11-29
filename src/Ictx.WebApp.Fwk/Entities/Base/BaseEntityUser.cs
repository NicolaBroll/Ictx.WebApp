using System;

namespace Ictx.WebApp.Fwk.Entities.Base
{
    public abstract class BaseEntityUser<TKey> : BaseEntity<TKey>
    {
        public Guid UserId { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} UserId: {UserId}";
        }
    }
}
