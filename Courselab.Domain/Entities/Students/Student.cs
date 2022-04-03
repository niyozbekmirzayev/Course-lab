using Courselab.Domain.Commons;

namespace Courselab.Domain.Entities.Students
{
    public class Student : Person
    {
        public virtual RegistratedCourses RegistratedCourses { get; set; }
    }
}
