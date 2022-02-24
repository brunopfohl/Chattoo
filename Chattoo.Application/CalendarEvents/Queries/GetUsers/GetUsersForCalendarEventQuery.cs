using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.Users.DTOs;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;

namespace Chattoo.Application.CalendarEvents.Queries
{
    public class GetUsersForCalendarEventQuery : PaginatedQuery<UserDto>
    {
        public string EventId { get; set; }
    }
    
    public class GetUsersForCalendarEventQueryHandler : PaginatedQueryHandler<GetUsersForCalendarEventQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly CalendarEventManager _eventManager;

        public GetUsersForCalendarEventQueryHandler(IMapper mapper,
            IUserRepository userRepository, CalendarEventManager eventManager)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _eventManager = eventManager;
        }

        public override async Task<PaginatedList<UserDto>> Handle(GetUsersForCalendarEventQuery request, CancellationToken cancellationToken)
        {
            // Pokusím se načíst kanál.
            var channel = await _eventManager.GetEventOrThrow(request.EventId);
            
            // Načtu uživatele z komunikačního kanálu.
            var users = _userRepository.GetByCalendarEventId(channel.Id);

            // Načtu kolekci uživatelů v dané skupině a zpracuju na stránkovanou kolekci.
            var result = await users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}