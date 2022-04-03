using Courselab.Service.DTOs.Commons;
using System;
using System.ComponentModel.DataAnnotations;

namespace Courselab.Service.DTOs.Authors
{
    public class AuthorForUpdateDto : PersonDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
