using Courselab.Domain.Enums;
using System;

namespace Courselab.Domain.Commons
{
    public abstract class Person : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BrithDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
