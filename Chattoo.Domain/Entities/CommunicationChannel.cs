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
    public class CommunicationChannel : AuditableEntity, IAuditableEntity, IAggregateRoot
    {
        protected List<CommunicationChannelMessage> _messages;
        protected List<CommunicationChannelRole> _roles;
        protected List<UserToCommunicationChannel> _users;

        protected CommunicationChannel()
        {
            _messages = new List<CommunicationChannelMessage>();
            _roles = new List<CommunicationChannelRole>();
            _users = new List<UserToCommunicationChannel>();
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
        public virtual IReadOnlyCollection<CommunicationChannelMessage> Messages => _messages.AsReadOnly();

        /// <summary>
        /// Vrací nebo nastavuje kolekci dostupných rolí pro uživatele v tomto kanálu (např. admin, moderátor, quest,...).
        /// </summary>
        public virtual IReadOnlyCollection<CommunicationChannelRole> Roles => _roles.AsReadOnly();

        public virtual IReadOnlyCollection<UserToCommunicationChannel> Users => _users.AsReadOnly();

        internal static CommunicationChannel Create(string name, string description)
        {
            var entity = new CommunicationChannel()
            {
                Name = name,
                Description = description
            };

            return entity;
        }

        #region Setters

        public void SetName(string name)
        {
            Name = name;
        }
        
        public void SetDescription(string description)
        {
            Description = description;
        }

        #endregion

        #region Roles
        
        public bool HasRole(string roleName)
        {
            return Roles.Any(r => r.Name == Name);
        }
        
        public CommunicationChannelRole AddRole(string name, CommunicationChannelPermission permission)
        {
            if(HasRole(name))
            {
                throw new DuplicitChannelRoleNameException(Id, name);
            }
            
            var role = CommunicationChannelRole.Create(name, Id, permission);
            
            _roles.Add(role);

            return role;
        }

        internal CommunicationChannelRole GetRole(string roleId)
        {
            var role = Roles.FirstOrDefault(r => r.Id == roleId);
            
            return role;
        }
        
        public void DeleteRole(CommunicationChannelRole role)
        {
            bool wasRemoved = _roles.Remove(role);

            if (!wasRemoved)
            {
                throw new ChannelRoleNotFound(Id, role.Id);
            }
        }

        #endregion

        #region Participants

        public bool HasParticipant(string participantId)
        {
            return Users.Any(u => u.UserId == participantId);
        }
        
        internal void AddParticipant(string participantId)
        {
            if (HasParticipant(participantId))
            {
                throw new DuplicitUserInChannelException(participantId, Id);
            }
            
            var participant = UserToCommunicationChannel.Create(participantId, Id);
            
            _users.Add(participant);
        }

        public void RemoveParticipant(string participantId)
        {
            var participant = Users.FirstOrDefault(m => m.UserId == participantId)
                ?? throw new UserNotFoundException(participantId);

            _users.Remove(participant);
        }

        #endregion

        #region Messages

        
        #endregion

        public CommunicationChannelMessage AddMessage(string authorId, string content,
            CommunicationChannelMessageType type)
        {
            var message = CommunicationChannelMessage.Create(authorId, Id, content, type);
            
            _messages.Add(message);

            return message;
        }
        
        
        internal void DeleteMessage(CommunicationChannelMessage message)
        {
            bool wasRemoved = _messages.Remove(message);

            if (!wasRemoved)
            {
                throw new MessageNotFoundException(message.Id);
            }
        }

        public CommunicationChannelMessage GetMessage(string messageId)
        {
            var message = Messages.FirstOrDefault(m => m.Id == messageId);

            return message;
        }
    }
}
