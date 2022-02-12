using System;
using System.Threading.Tasks;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Repositories
{
    public interface ICalendarEventTypeRepository
    {
        public async Task<CalendarEventType> GetByIdAsync(string typeId)
        {
            throw new NotImplementedException();
        }
        
        public CalendarEventType GetById(string typeId);
    }
}