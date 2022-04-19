using Courselab.Domain.CustomAttributes;
using Courselab.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Courselab.Service.DTOs.Commons
{
    public class PersonDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public DateTime DataOfBirh { get; set; }
        public string About { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [FileType(new string[] { ".png", ".jpg" })]
        [MaxFileSize(500)]
        public IFormFile Image { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [Password(8)]
        public string Password { get; set; }
    }
}
