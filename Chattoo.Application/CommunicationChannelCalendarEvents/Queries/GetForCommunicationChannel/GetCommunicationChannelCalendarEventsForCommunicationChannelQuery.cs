using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.CommunicationChannelCalendarEvents.DTOs;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.CommunicationChannelCalendarEvents.Queries.GetForCommunicationChannel
{
    /// <summary>
    /// Dotaz na kalendářní události z komunikačního kanálu.
    /// </summary>
    public class GetCommunicationChannelCalendarEventsForCommunicationChannelQuery : PaginatedQuery<CommunicationChannelCalendarEventDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, jehož kalendářní události se mají vrátit.
        /// </summary>
        public string CommunicationChannelId { get; set; }
    }

    public class GetCommunicationChannelCalendarEventsForUserQueryHandler : PaginatedQueryHandler<GetCommunicationChannelCalendarEventsForCommunicationChannelQuery, CommunicationChannelCalendarEventDto>
    {
        private readonly IMapper _mapper;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly ICommunicationChannelCalendarEventRepository _communicationChannelCalendarEventRepository;

        public GetCommunicationChannelCalendarEventsForUserQueryHandler(IMapper mapper, ICommunicationChannelCalendarEventRepository communicationChannelCalendarEventRepository, ICommunicationChannelRepository communicationChannelRepository)
        {
            _mapper = mapper;
            _communicationChannelCalendarEventRepository = communicationChannelCalendarEventRepository;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public override async Task<PaginatedList<CommunicationChannelCalendarEventDto>> Handle(GetCommunicationChannelCalendarEventsForCommunicationChannelQuery request, CancellationToken cancellationToken)
        {
            // Ověřím, zda-li komunikační kanál skutečně existuje.
            _communicationChannelRepository.ThrowIfNotExists(request.CommunicationChannelId);

            // Načtu kolekci kalendářních událostí komunikačního kanálu a zpracuju na stránkovanou kolekci.
            var result = await _communicationChannelCalendarEventRepository.GetByCommunicationChannelId(request.CommunicationChannelId)
                .ProjectTo<CommunicationChannelCalendarEventDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}