using Lab07.UnitTesting.DAL.Core.Context;
using Lab07.UnitTesting.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Lab07.UnitTesting.DAL.Interfaces.Repositories;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Lab07.UnitTesting.DAL.Repositories
{
    [ExcludeFromCodeCoverage]
    public class StoreTypeReposiroty : BaseRepository<StoreType>, IStoreTypeRepository
    {
        public StoreTypeReposiroty(GodelBenefitContext context)
            : base(context)
        {
        }

        public IList<StoreType> GetAllStoreType()
        {
            return entities.ToList();
        }

        public StoreType GetByName(string storeTypeName)
        {
            return entities.FirstOrDefault(x => x.Name.Equals(storeTypeName, StringComparison.OrdinalIgnoreCase));
        }
    }
}