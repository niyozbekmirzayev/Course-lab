using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courselab.Service.DTOs.Commons
{
    public class SetImageDto
    {
        public SetImageDto() 
        {

        }

        [Required]
        public Guid Id { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
