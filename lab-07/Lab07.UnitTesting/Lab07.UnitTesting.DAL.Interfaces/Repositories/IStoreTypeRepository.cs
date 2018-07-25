using System.Collections.Generic;
using Lab07.UnitTesting.DAL.Models;

namespace Lab07.UnitTesting.DAL.Interfaces.Repositories
{
    public interface IStoreTypeRepository : IBaseRepository<StoreType>
    {
        IList<StoreType> GetAllStoreType();

        StoreType GetByName(string storeTypeName);
    }
}