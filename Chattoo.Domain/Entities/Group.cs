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

        public static Group Create(string name)
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
                throw new Exception($"Role {name} already exists in group with id {Id}");
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

        public GroupRole DeleteRole(string roleId)
        {
            var role = GetRole(roleId);

            _roles.Remove(role);

            return role;
        }

        public bool HasRole(string name)
        {
            return Roles.Any(r => r.Name == name);
        }
        
        public void AddParticipant(string userId)
        {
            if (Participants.Any(p => p.UserId == userId))
            {
                throw new Exception($"Couldn't add. {nameof(User)}:{userId} is already in {nameof(Group)}:{Id}");
            }

            var participant = UserToGroup.Create(userId, Id);
            
            _participants.Add(participant);
        }

        public void RemoveParticipant(string participantId)
        {
            var participant = Participants.FirstOrDefault(p => p.UserId == participantId);

            if (participant == null)
            {
                throw new NotFoundException($"{nameof(Group)}:{nameof(User)}", participantId);
            }

            _participants.Remove(participant);
        }
        
        private GroupRole GetRole(string roleId)
        {
            var role = Roles.FirstOrDefault(r => r.Id == roleId);

            if (role == null)
            {
                throw new NotFoundException($"{nameof(Group)}:{nameof(GroupRole)}", roleId);
            }

            return role;
        }
    }
}
