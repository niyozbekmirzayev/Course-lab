using Courselab.Domain.CustomAttributes;
using Courselab.Domain.Enums;
using Microsoft.AspNetCore.Http;
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
        public IFormFile Image { get; set; }
        public IFormFile GuidVideo { get; set; }
        [Required]
        public CourseType Type { get; set; }
        [Required]
        public CourseLevel Level { get; set; }
        [Required]
        [YouTubeLink]
        public string YouTubePlayListLink { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
    }
}
