using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants57Blocks.Domain.Request
{
    public class UserRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
    }
}
