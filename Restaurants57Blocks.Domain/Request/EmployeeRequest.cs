using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants57Blocks.Domain.Request
{
    public class EmployeeRequest
    {
        public string Identifcation { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ResidenceAdress { get; set; }
        public string RestaurantId { get; set; }
        public int Type { get; set; }
    }
}
