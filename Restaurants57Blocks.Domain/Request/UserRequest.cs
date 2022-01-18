using Restaurants57Blocks.Common.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants57Blocks.Domain.Request
{
    public class UserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int EmployeeId { get; set; }
    }
}
