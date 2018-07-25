using System.Collections.Generic;
using System.Linq;
using Lab07.UnitTesting.DAL.Core.Context;
using Lab07.UnitTesting.DAL.Interfaces.Repositories;
using Lab07.UnitTesting.DAL.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Lab07.UnitTesting.DAL.Repositories
{
    [ExcludeFromCodeCoverage]
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(GodelBenefitContext context)
           : base(context)
        {
        }

        public IList<Store> GetAllByStoreTypeId(int storeTypeId)
        {
            var result = entities.Where(x => x.StoreTypeId == storeTypeId);
            return result.ToList();
        }

        public Store GetByName(string storeName)
        {
            return entities.FirstOrDefault(x => x.Name.Equals(storeName, StringComparison.OrdinalIgnoreCase));
        }
    }
}