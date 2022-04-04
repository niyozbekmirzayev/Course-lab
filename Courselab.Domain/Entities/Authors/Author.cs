using Courselab.Domain.Commons;
using Courselab.Domain.Entities.Courses;
using System.Collections.Generic;

namespace Courselab.Domain.Entities.Authors
{
    public class Author : Person
    {
        public string About { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
