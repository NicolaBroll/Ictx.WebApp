using System;

namespace Ictx.WebApp.Core.Base
{
    public abstract class BaseEntityUser : BaseEntity
    {
        public Guid UserId { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} UserId: {UserId}";
        }
    }
}
