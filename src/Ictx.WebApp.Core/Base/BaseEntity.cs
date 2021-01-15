using System;

namespace Ictx.WebApp.Core.Base
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }

        public override string ToString()
        {
            return $"Id:{Id}";
        }
    }
}
