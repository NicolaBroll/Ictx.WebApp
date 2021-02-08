using System.Collections.Generic;

namespace Ictx.WebApp.Shared.Models.Response
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Data { get; }
        public int TotalCount { get; }

        public PaginatedResponse(IEnumerable<T> data, int totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }
    }
}
