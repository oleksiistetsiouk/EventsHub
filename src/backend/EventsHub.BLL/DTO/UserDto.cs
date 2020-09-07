using System;

namespace EventsHub.BLL.DTO
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
}
