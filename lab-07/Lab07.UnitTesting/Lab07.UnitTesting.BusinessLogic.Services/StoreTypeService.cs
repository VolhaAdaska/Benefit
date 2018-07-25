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
    public class StoreTypeService : BaseService, IStoreTypeService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public StoreTypeService(IUnitOfWork unitOfWork, IMapper mapper)
           : base(unitOfWork, mapper)
        {
        }

        public async Task<OperationDetails> AddStoreTypeAsync(StoreTypeDto storeTypeDto)
        {
            if (storeTypeDto == null)
            {
                Logger.Error("Something went wrong");
                return new OperationDetails(false, "Something went wrong", "StoreType");
            }

            StoreType storeType = mapper.Map<StoreTypeDto, StoreType>(storeTypeDto);

            try
            {
                if (unitOfWork.StoreTypeRepository.GetByName(storeType.Name) != null)
                {
                    Logger.Error("A Store type with this name already exists");
                    return new OperationDetails(false, "A Store type with this name already exists", "StoreType");
                }
                await unitOfWork.StoreTypeRepository.CreateAsync(storeType);
                await unitOfWork.SaveAsync();
                Logger.Info("Successfully added");
                return new OperationDetails(true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return new OperationDetails(false, "Unfortunately, something went wrong....", "StoreType");
            }
        }

        public async Task<OperationDetails> DeleteStoreTypeAsync(StoreTypeDto storeTypeDto)
        {
            if (storeTypeDto == null)
            {
                Logger.Error("Something went wrong");
                return new OperationDetails(false, "Something went wrong", "StoreType");
            }

            StoreType storeType = await unitOfWork.StoreTypeRepository.GetByIdAsync(storeTypeDto.Id);
            if (storeType == null)
            {
                Logger.Error("Store type not found");
                return new OperationDetails(false, "Store type not found", "StoreType");
            }

            try
            {
                await unitOfWork.StoreTypeRepository.DeleteAsync(storeType);
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

        public async Task<OperationDetails> UpdateStoreTypeAsync(StoreTypeDto storeTypeDto)
        {
            if (storeTypeDto == null)
            {
                Logger.Error("Something went wrong");
                return new OperationDetails(false, "Something went wrong", "StoreType");
            }

            StoreType storeType = mapper.Map<StoreTypeDto, StoreType>(storeTypeDto);
            try
            {
                await unitOfWork.StoreTypeRepository.UpdateAsync(storeType);
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

        public IList<StoreTypeDto> GetAllStoreType()
        {
            var result = unitOfWork.StoreTypeRepository.GetAllStoreType();
            return mapper.Map<IList<StoreType>, IList<StoreTypeDto>>(result);
        }

        public async Task<StoreTypeDto> GetStoreTypeByIdAsync(int id)
        {
            StoreType storeType = await unitOfWork.StoreTypeRepository.GetByIdAsync(id);
            return mapper.Map<StoreType, StoreTypeDto>(storeType);
        }

    }
}