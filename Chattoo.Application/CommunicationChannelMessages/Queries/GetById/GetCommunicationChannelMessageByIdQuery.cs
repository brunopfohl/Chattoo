using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.CommunicationChannelMessages.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelMessages.Queries.GetById
{
    /// <summary>
    /// Dotaz na zprávu (z komunikačního kanálu) s daným Id.
    /// </summary>
    public class GetCommunicationChannelMessageByIdQuery : IRequest<CommunicationChannelMessageDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id zprávy z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }
    
    public class GetCommunicationChannelMessageByIdQueryHandler : IRequestHandler<GetCommunicationChannelMessageByIdQuery, CommunicationChannelMessageDto>
    {
        private readonly IMapper _mapper;

        public GetCommunicationChannelMessageByIdQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<CommunicationChannelMessageDto> Handle(GetCommunicationChannelMessageByIdQuery request, CancellationToken cancellationToken)
        {
            // // Načtu zprávu z datového zdroje (vyhodím výjimku, pokud se mi ji nepodaří dohledat).
            // var message = await _communicationChannelMessageRepository.GetByIdAsync<CommunicationChannelMessageDto>(request.Id, true);
            //
            // return message;
            return null;
        }
    }
}