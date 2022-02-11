using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Services;
using Chattoo.Application.Groups.DTOs;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.Groups.Commands
{
    /// <summary>
    /// Příkaz pro upravení již existující skupiny.
    /// </summary>
    public class UpdateGroupCommand : IRequest<GroupDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny uživatelů.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název skupiny uživatelů.
        /// </summary>
        public string Name { get; set; }
    }
    
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, GroupDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly GroupManager _groupManager;

        public UpdateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, GroupManager groupManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _groupManager = groupManager;
        }

        public async Task<GroupDto> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupManager.GetGroupOrThrow(request.Id);
            
            group.SetName(request.Name);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GroupDto>(group);
        }
    }
}