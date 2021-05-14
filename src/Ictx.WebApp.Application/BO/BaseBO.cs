using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Ictx.WebApp.Core.Models;
using System.Linq;

namespace Ictx.WebApp.Application.BO
{
    public abstract class BaseBO<T, K, Q> where Q : PaginationModel
    {    
        protected ILogger Logger { get; }

        public BaseBO(ILogger logger)
        {
            this.Logger = logger;
        }

        // Read many.
        public async Task<PageResult<T>> ReadManyAsync(Q filter)
        {
            try
            {
                return await ReadManyViewsAsync(filter);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, $"{GetBoName()} ReadManyAsync filter: {filter}");

                return new PageResult<T>
                {
                    Data = new List<T>(),
                    TotalCount = 0
                };
            }
        }

        protected virtual async Task<PageResult<T>> ReadManyViewsAsync(Q filter)
        {
            return await Task.FromException<PageResult<T>>(new NotImplementedException());
        }


        // Read many.
        public async Task<OperationResult<T>> ReadAsync(K key)
        {
            try
            {
                return await ReadViewAsync(key);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, $"{GetBoName()} ReadAsync key: {key}");

                return new OperationResult<T>(ex);
            }
        }

        protected virtual async Task<OperationResult<T>> ReadViewAsync(K key)
        {
            return await Task.FromException<OperationResult<T>>(new NotImplementedException());
        }


        // Delete.
        public async Task<OperationResult<bool>> DeleteAsync(K key)
        {
            try
            {
                return await DeleteViewAsync(key);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, $"{GetBoName()} DeleteAsync key: {key}");

                return new OperationResult<bool>(ex);
            }
        }

        protected virtual async Task<OperationResult<bool>> DeleteViewAsync(K key)
        {
            return await Task.FromException<OperationResult<bool>>(new NotImplementedException());
        }

        // Save.
        public async Task<OperationResult<T>> SaveAsync(K key, T value)
        {
            try
            {
                return await SaveViewAsync(key, value);

            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, $"{GetBoName()} SaveAsync key: {key} value: {value}");

                return new OperationResult<T>(ex);
            }
        }

        protected virtual async Task<OperationResult<T>> SaveViewAsync(K key, T value)
        {
            return await Task.FromException<OperationResult<T>>(new NotImplementedException());
        }


        // Insert.
        public async Task<OperationResult<T>> InsertAsync(T value)
        {
            try
            {
                return await InsertViewAsync(value);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, $"{GetBoName()} SaveAsync value: {value}");

                return new OperationResult<T>(ex);
            }
        }

        protected virtual async Task<OperationResult<T>> InsertViewAsync(T value)
        {
            return await Task.FromException<OperationResult<T>>(new NotImplementedException());
        }

        private string GetBoName() => String.Join(' ', this.Logger.GetType().GetGenericArguments().Select(x => x.Name)); 
    }
}