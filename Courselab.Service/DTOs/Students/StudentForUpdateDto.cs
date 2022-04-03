using Courselab.Service.DTOs.Commons;
using System;
using System.ComponentModel.DataAnnotations;

namespace Courselab.Service.DTOs.Students
{
    public class StudentForUpdateDto : PersonDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
