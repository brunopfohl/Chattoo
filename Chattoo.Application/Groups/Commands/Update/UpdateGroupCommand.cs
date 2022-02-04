using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Services;
using Chattoo.Application.Groups.DTOs;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Groups.Commands.Update
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
        private readonly IGroupRepository _groupRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;

        public UpdateGroupCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, GetByIdUserSafeService getByIdUserSafeService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _getByIdUserSafeService = getByIdUserSafeService;
            _mapper = mapper;
        }

        public async Task<GroupDto> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _getByIdUserSafeService.GetAsync(_groupRepository, request.Id);
            
            group.SetName(request.Name);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GroupDto>(group);
        }
    }
}