using Ictx.WebApp.Application.Repositories;
using Ictx.WebApp.Core.Entities;

namespace Ictx.WebApp.Application.UnitOfWork
{
    public interface IBackgroundServiceUnitOfWork : IUnitOfWorkBase
    {
        IOperationRepository OperationRepository { get; }
    }
}
