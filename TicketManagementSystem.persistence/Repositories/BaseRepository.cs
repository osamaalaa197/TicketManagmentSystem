using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Domain.Comman;

namespace TicketManagementSystem.persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly TicketManagementSystemDbContext _dbcontext;
        public BaseRepository(TicketManagementSystemDbContext dbContext)
        {
            _dbcontext = dbContext; 
        }
        public async Task<T> AddAysnc(T entity)
        {
             await _dbcontext.AddAsync(entity);
             await _dbcontext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAysnc(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbcontext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbcontext.Set<T>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }
        public async Task<T> UpdateAysnc(T entity)
        {
            _dbcontext.Entry(entity).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> SoftDeleteAysncAsync(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.IsDeleted = true;
                baseEntity.DateDeleted = DateTime.UtcNow;

                _dbcontext.Set<T>().Update(entity);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
