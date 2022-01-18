using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants57Blocks.Domain.Dto
{
    public class EmployeeDto
    {
        public int Identification { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ResidenceAdress { get; set; }
        public string RestaurantId { get; set; }
        public int Type { get; set; }
    }
}
