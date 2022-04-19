using Courselab.Domain.Commons;
using Courselab.Domain.Entities.Courses;
using Courselab.Domain.Entities.Registraions;
using System.Collections.Generic;

namespace Courselab.Domain.Entities.Users
{
    public class User : Person
    {
        public User()
        {
            Registrations = new List<Registration>();
            Courses = new List<Course>();
        }

        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
