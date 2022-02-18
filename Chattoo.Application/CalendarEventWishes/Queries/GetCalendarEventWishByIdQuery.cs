using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.CalendarEventWishes.DTOs;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CalendarEventWishes.Queries
{
    public class GetCalendarEventWishByIdQuery : IRequest<CalendarEventWishDto>
    {
        public string Id { get; set; }
    }
    
    public class GetCalendarEventWishByIdQueryHandler : IRequestHandler<GetCalendarEventWishByIdQuery, CalendarEventWishDto>
    {
        private readonly IMapper _mapper;
        private readonly CalendarEventWishManager _wishManager;

        public GetCalendarEventWishByIdQueryHandler(IMapper mapper, CalendarEventWishManager wishManager)
        {
            _mapper = mapper;
            _wishManager = wishManager;
        }

        public async Task<CalendarEventWishDto> Handle(GetCalendarEventWishByIdQuery request, CancellationToken cancellationToken)
        {
            var wish = await _wishManager.GetWishOrThrow(request.Id);

            return _mapper.Map<CalendarEventWishDto>(wish);
        }
    }
}