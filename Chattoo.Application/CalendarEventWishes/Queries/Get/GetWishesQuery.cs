using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.CalendarEventWishes.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.CalendarEventWishes.Queries
{
    public class GetWishesQuery : PaginatedQuery<CalendarEventWishDto>
    {
    }
    
    public class GetWishesQueryHandler : PaginatedQueryHandler<GetWishesQuery, CalendarEventWishDto>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICalendarEventWishRepository _wishRepository;

        public GetWishesQueryHandler(IMapper mapper, ICalendarEventWishRepository wishRepository, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _wishRepository = wishRepository;
            _currentUserService = currentUserService;
        }

        public override async Task<PaginatedList<CalendarEventWishDto>> Handle(GetWishesQuery request, CancellationToken cancellationToken)
        {
            var wishes = _wishRepository.GetActiveByUserId(_currentUserService.User.Id);
            
            var result = await wishes
                .ProjectTo<CalendarEventWishDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}