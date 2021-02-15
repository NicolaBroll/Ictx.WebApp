﻿using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ictx.WebApp.Api.Database.SeedData
{
    public class SeedUfficioBase : SeedDataCore
    {
        public SeedUfficioBase(AppUnitOfWork appUnitOfWork) : base(appUnitOfWork)
        { }

        public override async Task Popola()
        {
            await this._appUnitOfWork.UfficioBaseRepository.InsertManyAsync(GetUfficiBase());
            await this._appUnitOfWork.SaveAsync();
        }

        private List<UfficioBase> GetUfficiBase()
        {
            return new List<UfficioBase>()
            {
                new UfficioBase(1, "Ufficio base test 1"),
                new UfficioBase(2, "Ufficio base test 2"),
                new UfficioBase(3, "Ufficio base test 3")
            };
        }
    }
}
