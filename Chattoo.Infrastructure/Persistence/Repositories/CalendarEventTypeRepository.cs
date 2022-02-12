using System.Linq;
using System.Threading.Tasks;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    public class CalendarEventTypeRepository : ICalendarEventTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public CalendarEventTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<CalendarEventType> GetByIdAsync(string typeId)
        {
            return await _dbContext.Set<CalendarEventType>().FirstOrDefaultAsync(t => t.Id == typeId);
        }

        public CalendarEventType GetById(string typeId)
        {
            return _dbContext.Set<CalendarEventType>().FirstOrDefault(t => t.Id == typeId);
        }
    }
}