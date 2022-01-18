using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants57Blocks.Domain.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime DateRegister { get; set; }
        public bool Status { get; set; } = true;
        public int EmployeeId { get; set; }
    }
}
