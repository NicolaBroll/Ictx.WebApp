using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Application.BO
{
    public abstract class BaseBO<T, K, Q> where Q : PaginationModel
    {
        private readonly ILogger _logger;

        public BaseBO(ILogger logger)
        {
            this._logger = logger;
        }

        // Read many.
        public async Task<PageResult<T>> ReadManyAsync(Q filter, CancellationToken cancellationToken = default)
        {
            try
            {
                return await ReadManyViewsAsync(filter, cancellationToken);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"{GetBoName()} ReadManyAsync filter: {filter}");

                return new PageResult<T>
                {
                    Data = new List<T>(),
                    TotalCount = 0
                };
            }
        }

        protected virtual async Task<PageResult<T>> ReadManyViewsAsync(Q filter, CancellationToken cancellationToken)
        {
            return await Task.FromException<PageResult<T>>(new NotImplementedException());
        }


        // Read.
        public async Task<OperationResult<T>> ReadAsync(K key, CancellationToken cancellationToken = default)
        {
            try
            {
                return await ReadViewAsync(key, cancellationToken);
            }
            catch (TaskCanceledException) 
            {
                return new OperationResult<T>(new TaskCanceledException("Operazione annullata dall'utente."));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"{GetBoName()} ReadAsync key: {key}");
                return new OperationResult<T>(ex);
            }
        }

        protected virtual async Task<OperationResult<T>> ReadViewAsync(K key, CancellationToken cancellationToken)
        {
            return await Task.FromException<OperationResult<T>>(new NotImplementedException());
        }


        // Delete.
        public async Task<OperationResult<bool>> DeleteAsync(K key, CancellationToken cancellationToken = default)
        {
            try
            {
                return await DeleteViewAsync(key, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                return new OperationResult<bool>(new TaskCanceledException("Operazione annullata dall'utente."));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"{GetBoName()} DeleteAsync key: {key}");

                return new OperationResult<bool>(ex);
            }
        }

        protected virtual async Task<OperationResult<bool>> DeleteViewAsync(K key, CancellationToken cancellationToken)
        {
            return await Task.FromException<OperationResult<bool>>(new NotImplementedException());
        }


        // Save.
        public async Task<OperationResult<T>> SaveAsync(K key, T value, CancellationToken cancellationToken = default)
        {
            try
            {
                var validazione = Validation(value);

                if (validazione.IsFail) 
                {
                    return validazione;
                }

                return await SaveViewAsync(key, value, cancellationToken);

            }
            catch (TaskCanceledException)
            {
                return new OperationResult<T>(new TaskCanceledException("Operazione annullata dall'utente."));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"{GetBoName()} SaveAsync key: {key} value: {value}");

                return new OperationResult<T>(ex);
            }
        }

        protected virtual async Task<OperationResult<T>> SaveViewAsync(K key, T value, CancellationToken cancellationToken)
        {
            return await Task.FromException<OperationResult<T>>(new NotImplementedException());
        }


        // Insert.
        public async Task<OperationResult<T>> InsertAsync(T value, CancellationToken cancellationToken = default)
        {
            try
            {
                var validazione = Validation(value);

                if (validazione.IsFail)
                {
                    return validazione;
                }

                return await InsertViewAsync(value, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                return new OperationResult<T>(new TaskCanceledException("Operazione annullata dall'utente."));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"{GetBoName()} SaveAsync value: {value}");

                return new OperationResult<T>(ex);
            }
        }

        protected virtual async Task<OperationResult<T>> InsertViewAsync(T value, CancellationToken cancellationToken)
        {
            return await Task.FromException<OperationResult<T>>(new NotImplementedException());
        }


        // Validation.
        public OperationResult<T> Validation(T value)
        {
            return ValidationView(value);
        }

        protected abstract OperationResult<T> ValidationView(T value);

        private string GetBoName() => String.Join(' ', this._logger.GetType().GetGenericArguments().Select(x => x.Name)); 
    }
}