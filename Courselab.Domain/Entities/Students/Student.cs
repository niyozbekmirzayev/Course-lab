using Courselab.Domain.Commons;
using Courselab.Domain.Entities.Registraions;
using System.Collections.Generic;

namespace Courselab.Domain.Entities.Students
{
    public class Student : Person
    {
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
