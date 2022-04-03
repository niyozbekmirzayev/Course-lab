using System;
using System.ComponentModel.DataAnnotations;

namespace Courselab.Service.DTOs.Courses
{
    public class CourseForUpdateDto : CourseForCreationDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
