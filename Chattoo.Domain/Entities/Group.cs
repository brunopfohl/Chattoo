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
    public class Group : AuditableEntity, IAuditableEntity, IAggregateRoot,
        IWithRestrictedReadPermissions, IWithRestrictedWritePermissions
    {
        protected Group()
        {
            Roles = new List<GroupRole>();
            Participants = new List<UserToGroup>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje název skupiny uživatelů.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci uživatelských rolí, které jsou dostupné v této skupině.
        /// </summary>
        public virtual ICollection<GroupRole> Roles { get; set; }
        
        public virtual ICollection<UserToGroup> Participants { get; set; }

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
            if (Roles.Any(r => r.Name == name))
            {
                throw new Exception($"Role {name} already exists in group with id {Id}");
            }
            
            var role = GroupRole.Create(Id, name, permission);
            
            Roles.Add(role);

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

            Roles.Remove(role);

            return role;
        }
        
        public void AddParticipant(string userId)
        {
            if (Participants.Any(p => p.UserId == userId))
            {
                throw new Exception($"Couldn't add. {nameof(User)}:{userId} is already in {nameof(Group)}:{Id}");
            }

            var participant = UserToGroup.Create(userId, Id);
            
            Participants.Add(participant);
        }

        public void RemoveParticipant(string participantId)
        {
            var participant = Participants.FirstOrDefault(p => p.UserId == participantId);

            if (participant == null)
            {
                throw new NotFoundException($"{nameof(Group)}:{nameof(User)}", participantId);
            }

            Participants.Remove(participant);
        }
        
        #region IWithRestrictedReadPermissions

        ICollection<string> IWithRestrictedReadPermissions.UsersIds =>
            Participants.Select(u => u.UserId).ToList();

        #endregion
        
        #region IWithRestrictedWritePermissions

        ICollection<string> IWithRestrictedWritePermissions.UsersIds =>
            Participants.Select(u => u.UserId).ToList();

        #endregion

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
