using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Contract.Persistence
{
    public interface IAsyncRepository <T>where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAysnc(T entity);
        Task<T> UpdateAysnc(T entity);
        Task<bool> DeleteAysnc(T entity);
        Task<bool> SoftDeleteAysncAsync(T entity);
    }
}
