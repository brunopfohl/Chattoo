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
    /// Entita komunikačního kanálu sloužícího pro sdílení obsahu (zpráv) mezi uživateli.
    /// </summary>
    public class CommunicationChannel : AuditableEntity, IAuditableEntity, IAggregateRoot,
        IWithRestrictedReadPermissions, IWithRestrictedWritePermissions
    {
        protected CommunicationChannel()
        {
            Messages = new List<CommunicationChannelMessage>();
            Roles = new List<CommunicationChannelRole>();
            Users = new List<UserToCommunicationChannel>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje název komunikačního kanálu.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popis komunikačního kanálu.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Vrací nebo nastavuje kolekci zpráv, které byly mezi uživateli sdíleny pomocí tohoto komunikačního kanálu.
        /// </summary>
        public virtual ICollection<CommunicationChannelMessage> Messages { get; private set; }

        /// <summary>
        /// Vrací nebo nastavuje kolekci dostupných rolí pro uživatele v tomto kanálu (např. admin, moderátor, quest,...).
        /// </summary>
        public virtual ICollection<CommunicationChannelRole> Roles { get; private set; }
        
        public virtual ICollection<UserToCommunicationChannel> Users { get; private set; }

        #region IWithRestrictedReadPermissions

        ICollection<string> IWithRestrictedReadPermissions.UsersIds =>
            Users.Select(u => u.UserId).ToList();

        #endregion
        
        #region IWithRestrictedWritePermissions

        ICollection<string> IWithRestrictedWritePermissions.UsersIds =>
            Users.Select(u => u.UserId).ToList();

        #endregion

        public static CommunicationChannel Create(string name, string description)
        {
            var entity = new CommunicationChannel()
            {
                Name = name,
                Description = description
            };

            return entity;
        }

        public void SetName(string name)
        {
            Name = name;
        }
        
        public void SetDescription(string description)
        {
            Description = description;
        }

        public CommunicationChannelMessageAttachment AddAttachment(string messageId, string name,
            byte[] content, CommunicationChannelMessageAttachmentType type)
        {
            var message = GetMessage(messageId);

            var attachment = message.AddAttachment(name, content, type);

            return attachment;
        }

        public CommunicationChannelMessageAttachment DeleteAttachment(string messageId, string attachmentId)
        {
            var message = GetMessage(messageId);
            
            var attachment = message.DeleteAttachment(attachmentId);

            return attachment;
        }
        
        public CommunicationChannelMessageAttachment UpdateAttachment(string messageId, string attachmentId, string name)
        {
            var message = GetMessage(messageId);

            var attachment = message.UpdateAttachment(attachmentId, name);

            return attachment;
        }

        public CommunicationChannelRole AddRole(string name, CommunicationChannelPermission permission)
        {
            if (Roles.Any(r => r.Name == name))
            {
                throw new Exception($"Couldn't add Role with name '{name}' to channel '{Id}, because it already exists'");
            }
            
            var role = CommunicationChannelRole.Create(name, Id, permission);
            
            Roles.Add(role);

            return role;
        }
        
        public CommunicationChannelRole DeleteRole(string roleId)
        {
            var role = GetRole(roleId);
            
            Roles.Remove(role);

            return role;
        }

        public CommunicationChannelRole UpdateRole(string roleId, string name, CommunicationChannelPermission permission)
        {
            var role = GetRole(roleId);
            
            role.SetName(name);
            role.SetPermission(permission);
            
            return role;
        }

        public void AddParticipant(string participantId)
        {
            if (Users.Any(u => u.UserId == participantId))
            {
                throw new Exception($"Couldn't add User:{participantId} to CommunicationChannel:{Id}");
            }
            
            var participant = UserToCommunicationChannel.Create(participantId, Id);
            
            Users.Add(participant);
        }

        public void RemoveParticipant(string participantId)
        {
            var participant = Users.FirstOrDefault(m => m.UserId == participantId);

            if (participant == null)
            {
                throw new NotFoundException(
                    $"{nameof(CommunicationChannel)}:{nameof(User)}", participantId
                );
            }

            Users.Remove(participant);
        }

        public CommunicationChannelMessage AddMessage(string authorId, string content,
            CommunicationChannelMessageType type)
        {
            var message = CommunicationChannelMessage.Create(authorId, Id, content, type);
            
            Messages.Add(message);

            return message;
        }
        
        public CommunicationChannelMessage DeleteMessage(string id)
        {
            var message = GetMessage(id);

            Messages.Remove(message);

            return message;
        }
        
        public CommunicationChannelMessage UpdateMessage(string id, string content)
        {
            var message = GetMessage(id);
            
            message.SetContent(content);

            return message;
        }

        private CommunicationChannelMessage GetMessage(string messageId)
        {
            var message = Messages.FirstOrDefault(m => m.Id == messageId);

            if (message == null)
            {
                throw new NotFoundException(
                    $"{nameof(CommunicationChannel)}:{nameof(CommunicationChannelMessage)}", messageId
                );
            }

            return message;
        }

        private CommunicationChannelRole GetRole(string roleId)
        {
            var role = Roles.FirstOrDefault(r => r.Id == roleId);
            
            if (role == null)
            {
                throw new NotFoundException(
                    $"{nameof(CommunicationChannel)}:{nameof(CommunicationChannelRole)}", roleId
                );
            }

            return role;
        }
    }
}
