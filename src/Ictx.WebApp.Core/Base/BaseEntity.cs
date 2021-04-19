using System;

namespace Ictx.WebApp.Core.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}";
        }
    }
}
