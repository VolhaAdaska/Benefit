using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;
using Lab07.UnitTesting.DAL.Core.Context;
using Lab07.UnitTesting.DAL.Interfaces.Repositories;
using Lab07.UnitTesting.DAL.Models;
using System.Threading.Tasks;

namespace Lab07.UnitTesting.DAL.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity
    {
        protected readonly GodelBenefitContext context;
        protected readonly DbSet<T> entities;

        public BaseRepository(GodelBenefitContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Add(entity);
            await GetByIdAsync(entity.Id);
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.Remove(entity);
            await GetByIdAsync(entity.Id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entities.AddOrUpdate(entity);
            await GetByIdAsync(entity.Id);
        }
    }
}