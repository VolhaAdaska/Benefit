using System.Collections.Generic;
using Lab07.UnitTesting.DTO;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;
using System.Threading.Tasks;
using System;

namespace Lab07.UnitTesting.BusinessLogic.Interfaces
{
    public interface IStoreTypeService : IDisposable
    {
        Task<OperationDetails> AddStoreTypeAsync(StoreTypeDto storeTypeDto);

        Task<OperationDetails> UpdateStoreTypeAsync(StoreTypeDto storeTypeDto);

        Task<StoreTypeDto> GetStoreTypeByIdAsync(int id);

        Task<OperationDetails> DeleteStoreTypeAsync(StoreTypeDto storeTypeDto);

        IList<StoreTypeDto> GetAllStoreType();
    }
}
