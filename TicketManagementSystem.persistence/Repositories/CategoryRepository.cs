using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(TicketManagementSystemDbContext dbContext) : base(dbContext) 
        {
        }
        public async Task<List<Category>> GetCategoryWithEvent(bool IncludeHistory)
        {
            var allCategories=await _dbcontext.Categories.Include(e => e.Events).Where(e=>!e.IsDeleted).ToListAsync();
            if (!IncludeHistory)
            {
                allCategories.ForEach(e => e.Events.ToList().RemoveAll(e => e.EventDate < DateTime.Today));
            }
            return allCategories;
        }
    }
}
