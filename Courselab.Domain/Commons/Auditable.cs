using Courselab.Domain.Enums;
using System;

namespace Courselab.Domain.Commons
{
    public abstract class Auditable
    {
        public Guid Id { get; set; }
        public ObjectStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; } = null;
        public DateTime? LastModificationDate { get; set; } = null;

        public void Create()
        {
            Status = ObjectStatus.Created;
            CreatedDate = DateTime.Now;
        }

        public void Delete()
        {
            Status = ObjectStatus.Deleted;
            DeletedDate = DateTime.Now;
        }

        public void Modify()
        {
            Status = ObjectStatus.Modified;
            LastModificationDate = DateTime.Now;
        }
    }
}
