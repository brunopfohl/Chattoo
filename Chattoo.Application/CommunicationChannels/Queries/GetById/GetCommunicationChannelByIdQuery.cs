using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Queries.GetById
{
    /// <summary>
    /// Dotaz na komunikační kanál s daným Id.
    /// </summary>
    public class GetCommunicationChannelByIdQuery : IRequest<CommunicationChannelDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }
    
    public class GetCommunicationChannelByIdQueryHandler : IRequestHandler<GetCommunicationChannelByIdQuery, CommunicationChannelDto>
    {
        private readonly IMapper _mapper;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public GetCommunicationChannelByIdQueryHandler(IMapper mapper, ICommunicationChannelRepository communicationChannelRepository)
        {
            _mapper = mapper;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public async Task<CommunicationChannelDto> Handle(GetCommunicationChannelByIdQuery request, CancellationToken cancellationToken)
        {
            // Načtu komunikační kanál z datového zdroje.
            var channel = await _communicationChannelRepository.GetByIdAsync(request.Id);

            // Pokud se komunikační kanál s daným Id nepodařilo dohledat, vracím chybu.
            if (channel is null)
            {
                throw new NotFoundException(nameof(CommunicationChannel), request.Id);
            }

            // Převedu entitu na dto.
            var result = _mapper.Map<CommunicationChannelDto>(channel);

            return result;
        }
    }
}