using Ictx.WebApp.Application.UnitOfWork;
using Ictx.WebApp.Application.Repositories;
using Ictx.WebApp.Infrastructure.Data.BackgroundService;
using Ictx.WebApp.Infrastructure.Repositories;

namespace Ictx.WebApp.Infrastructure.UnitOfWork
{
    public class BackgroundServiceUnitOfWork : UnitOfWorkBase<BackgroundServiceDbContext>, IBackgroundServiceUnitOfWork
    {
        // Repository.
        private IOperationRepository _operationRepository;

        public BackgroundServiceUnitOfWork(BackgroundServiceDbContext context) : base(context)
        { }

        public IOperationRepository OperationRepository
        {
            get
            {
                if (this._operationRepository == null)
                {
                    this._operationRepository = new OperationRepository(this._context);
                }

                return this._operationRepository;
            }
        }    
    }
}