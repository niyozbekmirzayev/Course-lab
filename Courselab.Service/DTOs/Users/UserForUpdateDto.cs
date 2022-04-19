using System;

namespace Courselab.Service.DTOs.Users
{
    public class UserForUpdateDto : UserForCreationDto
    {
        public Guid Id { get; set; }
    }
}
