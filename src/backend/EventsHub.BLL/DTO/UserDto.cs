using System;
using System.Collections.Generic;
using System.Text;

namespace EventsHub.BLL.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
