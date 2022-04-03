using Courselab.Service.DTOs.Commons;
using System;

namespace Courselab.Service.DTOs.Students
{
    public class StudentForUpdateDto : PersonDto
    {
        public Guid Id { get; set; }
    }
}
