using System;
using System.Linq;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.Common.Services
{
    public class GroupValidationService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ICurrentUserService _currentUserService;

        private Group _group;
        
        public GroupValidationService(IGroupRepository groupRepository, ICurrentUserService currentUserService)
        {
            _groupRepository = groupRepository;
            _currentUserService = currentUserService;
        }
        
        public bool Found(string groupId)
        {
            _group = _groupRepository.GetById(groupId);

            return _group != null;
        }

        public bool ReadPermissionGranted(string channelId)
        {
            if (_group == null)
            {
                throw new InvalidOperationException(
                    $"'{nameof(ReadPermissionGranted)}' method can be called only after '{nameof(Found)}' has been called."
                );
            }
            
            return _currentUserService.CanViewGroup(_group);
        }

        public bool RoleFound(string roleId)
        {
            if (_group == null)
            {
                throw new InvalidOperationException(
                    $"'{nameof(RoleFound)}' method can be called only after '{nameof(Found)}' has been called."
                );
            }

            return _group.Roles.Any(r => r.Id == roleId);
        }

        public bool RoleCanBeAdded(string roleName)
        {
            if (_group == null)
            {
                throw new InvalidOperationException(
                    $"'{nameof(Found)}' method can be called only after '{nameof(Found)}' has been called."
                );
            }

            return !_group.HasRole(roleName);
        }
    }
}