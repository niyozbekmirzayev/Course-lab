using Courselab.Domain.Commons;
using System.Collections.Generic;

namespace Courselab.Domain.Entities.Students
{
    public class Student : Person
    {
        public virtual ICollection<RegistratedCourses> RegistratedCourses { get; set; }
    }
}
