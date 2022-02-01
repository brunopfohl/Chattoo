using System;

namespace Chattoo.Domain.Interfaces
{
    public interface IAuditableEntity
    {
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set;  }

        public DateTime? ModifiedAt { get; set;  }

        public string ModifiedBy { get; set; }

        public DateTime? DeletedAt { get; }

        public string DeletedBy { get; }
    }
}