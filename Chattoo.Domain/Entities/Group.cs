using System;
using System.Collections.Generic;
using System.Linq;
using Chattoo.Domain.Common;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita skupiny uživatelů.
    /// </summary>
    public class Group : AuditableEntity, IAuditableEntity, IAggregateRoot
    {
        private List<GroupRole> _roles;
        private List<UserToGroup> _participants;

        protected Group()
        {
            _roles = new List<GroupRole>();
            _participants = new List<UserToGroup>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje název skupiny uživatelů.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje kolekci uživatelských rolí, které jsou dostupné v této skupině.
        /// </summary>
        public virtual IReadOnlyCollection<GroupRole> Roles => _roles.AsReadOnly();

        public virtual IReadOnlyCollection<UserToGroup> Participants => _participants.AsReadOnly();

        internal static Group Create(string name)
        {
            var entity = new Group();
            
            entity.SetName(name);

            return entity;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public GroupRole AddRole(string name, UserGroupPermission permission)
        {
            if (HasRole(name))
            {
                throw new DuplicitGroupRoleNameException(Id, name);
            }
            
            var role = GroupRole.Create(Id, name, permission);
            
            _roles.Add(role);

            return role;
        }

        public GroupRole UpdateRole(string roleId, string name, UserGroupPermission permission)
        {
            var role = GetRole(roleId);
            
            role.SetName(name);
            role.SetPermission(permission);

            return role;
        }

        public void DeleteRole(GroupRole role)
        {
            bool wasRemoved = _roles.Remove(role);

            if (!wasRemoved)
            {
                throw new GroupRoleNotFoundException(Id, role.Id);
            }
        }

        public bool HasRole(string name)
        {
            return Roles.Any(r => r.Name == name);
        }

        public bool HasParticipant(string userId)
        {
            return Participants.Any(p => p.UserId == userId);
        }
        
        internal void AddParticipant(string userId)
        {
            if (HasParticipant(userId))
            {
                throw new DuplicitUserInGroupException(Id, userId);
            }

            var participant = UserToGroup.Create(userId, Id);
            
            _participants.Add(participant);
        }

        public void RemoveParticipant(string participantId)
        {
            var participant = Participants.FirstOrDefault(p => p.UserId == participantId)
                ?? throw new UserNotFoundException(participantId);

            _participants.Remove(participant);
        }
        
        internal GroupRole GetRole(string roleId)
        {
            var role = Roles.FirstOrDefault(r => r.Id == roleId);

            return role;
        }
    }
}
