using Lab07.UnitTesting.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab07.UnitTesting.DAL.Interfaces.Repositories
{
    public interface IStoreRepository : IBaseRepository<Store>
    {
        IList<Store> GetAllByStoreTypeId(int storeTypeId);

        Store GetByName(string storeName);
    }
}