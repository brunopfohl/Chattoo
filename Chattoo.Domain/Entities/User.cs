using System.Collections.Generic;
using Chattoo.Domain.Common;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita se záznamy o uživatelovi (není využíváno Identity službou).
    /// </summary>
    public class User : AuditableEntity, IAuditableEntity
    {
        public User()
        {
            Aliases = new List<UserAlias>();
            Groups = new List<Group>();
            GroupRoles = new List<GroupRole>();
            Channels = new List<CommunicationChannel>();
            ChannelMessages = new List<CommunicationChannelMessage>();
            ChannelRoles = new List<CommunicationChannelRole>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje uživatelské jméno.
        /// </summary>
        public virtual string UserName { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci aliasů (přezdívek), které má tento uživatel napříč aplikací.
        /// </summary>
        public virtual ICollection<UserAlias> Aliases { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci skupin, do kterých patří tento uživatel.
        /// </summary>
        public virtual ICollection<Group> Groups { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci rolí, kterými uživatel disponuje napříč všemi skupinami.
        /// </summary>
        public virtual ICollection<GroupRole> GroupRoles { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje komunikačních kanálů, kterých je uživatel součástí.
        /// </summary>
        public virtual ICollection<CommunicationChannel> Channels { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci zpráv, které uživatel sdílel napříč všemi skupinami.
        /// </summary>
        public virtual ICollection<CommunicationChannelMessage> ChannelMessages { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci rolí, kterými uživatel disponuje napříč všemi komunikačními kanály.
        /// </summary>
        public virtual ICollection<CommunicationChannelRole> ChannelRoles { get; set; }
    }
}