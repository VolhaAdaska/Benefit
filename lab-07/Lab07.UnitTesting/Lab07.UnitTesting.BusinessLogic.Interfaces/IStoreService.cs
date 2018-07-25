using System.Collections.Generic;
using Lab07.UnitTesting.DTO;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;
using System.Threading.Tasks;
using System;

namespace Lab07.UnitTesting.BusinessLogic.Interfaces
{
    public interface IStoreService : IDisposable
    {
        Task<OperationDetails> AddStoreAsync(StoreDto storeDto);

        Task<OperationDetails> DeleteStoreAsync(StoreDto storeDto);

        Task<OperationDetails> UpdateStoreAsync(StoreDto storeDto);

        Task<StoreDto> GetStoreByIdAsync(int id);

        IList<StoreDto> GetAllStore(int storeTypeId);
    }
}