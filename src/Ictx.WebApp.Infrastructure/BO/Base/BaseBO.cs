using System;
using System.Threading.Tasks;
using Ictx.WebApp.Infrastructure.Models;
using Ictx.Framework.Models;
using Ictx.WebApp.Infrastructure.BO.Base;

namespace Ictx.WebApp.Infrastructure.BO
{
    public abstract class BaseBO<T, K, Q> : IBaseBO<T, K, Q> where Q : ServiceParameters
    {
        // Read many.
        public async Task<PageResult<T>> ReadManyAsync(Q filter)
        {
            return await ReadManyViewsAsync(filter);
        }
        protected virtual async Task<PageResult<T>> ReadManyViewsAsync(Q filter)
        {
            return await Task.FromException<PageResult<T>>(new NotImplementedException());
        }


        // Read many.
        public async Task<BOResult<T>> ReadAsync(K key)
        {
            return await ReadViewAsync(key);
        }

        protected virtual async Task<BOResult<T>> ReadViewAsync(K key)
        {
            return await Task.FromException<BOResult<T>>(new NotImplementedException());
        }


        // Delete.
        public async Task<BOResult<bool>> DeleteAsync(K key)
        {
            return await DeleteViewAsync(key);
        }

        protected virtual async Task<BOResult<bool>> DeleteViewAsync(K key)
        {
            return await Task.FromException<BOResult<bool>>(new NotImplementedException());
        }

        // Save.
        public async Task<BOResult<T>> SaveAsync(K key, T value)
        {
            return await SaveViewAsync(key, value);
        }

        protected virtual async Task<BOResult<T>> SaveViewAsync(K key, T value)
        {
            return await Task.FromException<BOResult<T>>(new NotImplementedException());
        }


        // Insert.
        public async Task<BOResult<T>> InsertAsync(T value)
        {
            return await InsertViewAsync(value);
        }

        protected virtual async Task<BOResult<T>> InsertViewAsync(T value)
        {
            return await Task.FromException<BOResult<T>>(new NotImplementedException());
        }
    }
}