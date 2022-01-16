using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants57Blocks.Common.Constants
{
    public class RegularExpressions
    {
        public const string Password = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-._]).{10,}$";
        public const string Email = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
    }
}
