using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Courselab.Service.DTOs.Commons
{
    public class SetImageDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
