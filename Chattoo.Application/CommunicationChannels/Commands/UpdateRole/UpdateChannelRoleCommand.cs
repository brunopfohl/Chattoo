using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Příkaz pro upravení již existující role z komunikačního kanálu.
    /// </summary>
    public class UpdateChannelRoleCommand : IRequest<CommunicationChannelRoleDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id role z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název uživatelské role.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje práva uživatele, který disponuje touto rolí.
        /// </summary>
        public CommunicationChannelPermission Permission { get; set; }
    }
    
    public class UpdateChannelRoleCommandHandler : IRequestHandler<UpdateChannelRoleCommand, CommunicationChannelRoleDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public UpdateChannelRoleCommandHandler(IUnitOfWork unitOfWork, GetByIdUserSafeService getByIdUserSafeService, ICommunicationChannelRepository communicationChannelRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _getByIdUserSafeService = getByIdUserSafeService;
            _communicationChannelRepository = communicationChannelRepository;
            _mapper = mapper;
        }

        public async Task<CommunicationChannelRoleDto> Handle(UpdateChannelRoleCommand request, CancellationToken cancellationToken)
        {
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.ChannelId);

            var role = channel.UpdateRole(request.Id, request.Name, request.Permission);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CommunicationChannelRoleDto>(role);
        }
    }
}