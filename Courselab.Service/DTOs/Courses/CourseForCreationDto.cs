using Courselab.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Courselab.Service.DTOs.Courses
{
    public class CourseForCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public CourseType Type { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
    }
}
