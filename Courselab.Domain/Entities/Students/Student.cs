using Courselab.Domain.Commons;
using System.Collections.Generic;

namespace Courselab.Domain.Entities.Students
{
    public class Student : Person
    {
        public virtual RegistratedCourses RegistratedCourses { get; set; }
    }
}
