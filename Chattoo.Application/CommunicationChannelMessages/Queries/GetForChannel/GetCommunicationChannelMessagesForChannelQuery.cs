using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Mappings;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Common.Queries;
using Chattoo.Application.CommunicationChannelMessages.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.CommunicationChannelMessages.Queries.GetForChannel
{
    /// <summary>
    /// Dotaz na zprávy z komunikačního kanálu.
    /// </summary>
    public class GetCommunicationChannelMessagesForUserInChannelQuery : PaginatedQuery<CommunicationChannelMessageDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, ve kterém jsou hledány zprávy.
        /// </summary>
        public string ChannelId { get; set; }
    }

    public class GetCommunicationChannelMessagesForUserInChannelQueryHandler : PaginatedQueryHandler<GetCommunicationChannelMessagesForUserInChannelQuery, CommunicationChannelMessageDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly ICommunicationChannelMessageRepository _communicationChannelMessageRepository;

        public GetCommunicationChannelMessagesForUserInChannelQueryHandler(IMapper mapper, IUserRepository userRepository, ICommunicationChannelRepository communicationChannelRepository, ICommunicationChannelMessageRepository communicationChannelMessageRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _communicationChannelRepository = communicationChannelRepository;
            _communicationChannelMessageRepository = communicationChannelMessageRepository;
        }

        public override async Task<PaginatedList<CommunicationChannelMessageDto>> Handle(GetCommunicationChannelMessagesForUserInChannelQuery request, CancellationToken cancellationToken)
        {
            // Zjistím, zda-li komunikační kanál skutečně existuje.
            _communicationChannelRepository.ThrowIfNotExists(request.ChannelId);

            // Načtu kolekci rolí uživatele v komunikačním kanálu a zpracuju na stránkovanou kolekci.
            var result = await _communicationChannelMessageRepository.GetByChannelId(request.ChannelId)
                .ProjectTo<CommunicationChannelMessageDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return result;
        }
    }
}