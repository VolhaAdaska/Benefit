using AutoMapper;
using Lab07.UnitTesting.BusinessLogic.Infrastructure;
using Lab07.UnitTesting.BusinessLogic.Interfaces;
using Lab07.UnitTesting.DAL.Interfaces;
using Lab07.UnitTesting.DAL.Models;
using Lab07.UnitTesting.DTO;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab07.UnitTesting.BusinessLogic.Services
{
    public class StoreService : BaseService, IStoreService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public StoreService(IUnitOfWork unitOfWork, IMapper mapper)
           : base(unitOfWork, mapper)
        {
        }

        public async Task<OperationDetails> AddStoreAsync(StoreDto storeDto)
        {
            if (storeDto == null)
            {
                Logger.Error("Something went wrong");
                return new OperationDetails(false, "Something went wrong", "Store");
            }

            Store store = mapper.Map<StoreDto, Store>(storeDto);
            try
            {
                if (unitOfWork.StoreRepository.GetByName(store.Name) != null)
                {
                    Logger.Error("A Store with this name already exists");
                    return new OperationDetails(false, "A Store with this name already exists", "StoreType");
                }
                await unitOfWork.StoreRepository.CreateAsync(store);
                await unitOfWork.SaveAsync();
                Logger.Info("Successfully added");
                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return new OperationDetails(false, "Unfortunately, something went wrong....", "Store");
            }
        }

        public async Task<OperationDetails> DeleteStoreAsync(StoreDto storeDto)
        {
            if (storeDto == null)
            {
                Logger.Error("Something went wrong");
                return new OperationDetails(false, "Something went wrong", "Store");
            }

            Store store = await unitOfWork.StoreRepository.GetByIdAsync(storeDto.Id);
            if (store == null)
            {
                Logger.Error("Store not found");
                return new OperationDetails(false, "Store not found", "Store");
            }

            try
            {
                await unitOfWork.StoreRepository.DeleteAsync(store);
                await unitOfWork.SaveAsync();
                Logger.Info("Successfully deleted");
                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return new OperationDetails(false, ex.Message);
            }
        }

        public async Task<OperationDetails> UpdateStoreAsync(StoreDto storeDto)
        {
            if (storeDto == null)
            {
                Logger.Error("Something went wrong");
                return new OperationDetails(false, "Something went wrong", "Store");
            }

            Store store = mapper.Map<StoreDto, Store>(storeDto);

            try
            {
                await unitOfWork.StoreRepository.UpdateAsync(store);
                await unitOfWork.SaveAsync();
                Logger.Info("Successfully updated");
                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return new OperationDetails(false, ex.Message);
            }
        }

        public IList<StoreDto> GetAllStore(int storeTypeId)
        {
            var result = unitOfWork.StoreRepository.GetAllByStoreTypeId(storeTypeId);
            return mapper.Map<IList<Store>, IList<StoreDto>>(result);
        }

        public async Task<StoreDto> GetStoreByIdAsync(int id)
        {
            Store store = await unitOfWork.StoreRepository.GetByIdAsync(id);
            return mapper.Map<Store, StoreDto>(store);
        }
    }
}