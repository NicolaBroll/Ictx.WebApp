using Ictx.Framework.Models;
using Ictx.WebApp.Infrastructure.BO.Base;
using Ictx.WebApp.Infrastructure.Models;
using System.Threading.Tasks;

namespace Ictx.WebApp.Infrastructure.BO
{
    public interface IBaseBO<T, K, Q> where Q : ServiceParameters
    {
        Task<BOResult<bool>> DeleteAsync(K key);
        Task<BOResult<T>> InsertAsync(T value);
        Task<BOResult<T>> ReadAsync(K key);
        Task<PageResult<T>> ReadManyAsync(Q filter);
        Task<BOResult<T>> SaveAsync(K key, T value);
    }
}