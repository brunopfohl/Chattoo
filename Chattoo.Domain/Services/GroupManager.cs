using System.Linq;
using System.Threading.Tasks;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;

namespace Chattoo.Domain.Services
{
    public class GroupManager
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICalendarEventRepository _calendarEventRepository;
        
        public GroupManager(IGroupRepository groupRepository, ICurrentUserService currentUserService, IUserRepository userRepository, ICalendarEventRepository calendarEventRepository)
        {
            _groupRepository = groupRepository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _calendarEventRepository = calendarEventRepository;
        }

        public Group Create(string name)
        {
            var group = Group.Create(name);
            
            group.AddParticipant(_currentUserService.User.Id);

            return group;
        }
        
        public async Task<Group> GetGroupOrThrow(string groupId)
        {
            var group = await _groupRepository.GetByIdAsync(groupId)
                ?? throw new GroupNotFoundException(groupId);

            if (!_currentUserService.CanViewGroup(group))
            {
                throw new ForbiddenAccessException();
            }

            return group;
        }

        public GroupRole GetRoleOrThrow(Group group, string roleId)
        {
            return group.GetRole(roleId)
                   ?? throw new GroupRoleNotFoundException(group.Id, roleId);
        }

        public async Task AddParticipantToGroup(Group group, string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new UserNotFoundException(userId);
            
            group.AddParticipant(user.Id);
        }
    }
}