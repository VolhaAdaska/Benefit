using Lab07.UnitTesting.DAL.Interfaces;
using AutoMapper;
using System;

namespace Lab07.UnitTesting.BusinessLogic.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                unitOfWork.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}